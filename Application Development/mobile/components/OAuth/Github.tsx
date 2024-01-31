import React, { useState, useEffect } from 'react';
import { StyleSheet, Text, View, TouchableOpacity } from 'react-native';
import * as WebBrowser from 'expo-web-browser';
import AsyncStorage from '@react-native-async-storage/async-storage';
import request from '../../constants/request';

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'rgba(255, 255, 255, 0.0)',
    alignItems: 'center',
    justifyContent: 'center',
  },
  button: {
    padding: 16,
    backgroundColor: 'blue',
    borderRadius: 8,
  },
  buttonText: {
    color: 'white',
    fontSize: 18,
  },
});

export default function GitHubLogin() {
  const [userInformation, setUserInformation] = useState(null);
  const clientId = '4445d39c7975b7beb5b9';
  const [githubAuth, setGithubAuth] = useState(false);

  const fetchToken = async (code: string) => {
    try {
      // Exchange the code for a token using your API
      const responseData = await request.githubLogin({ code });

      if (Array.isArray(responseData) && responseData.length > 0) {
        const token = responseData[0];
        if (typeof token === 'string') {
          console.log('Access Token:', token);
          await AsyncStorage.setItem('authtoken', token);
          console.log('Token stored successfully');
        } else {
          console.error('Invalid token data');
        }
      } else {
        console.error('Invalid response data');
      }
    } catch (error) {
      console.error('Error during token retrieval:', error);
    }
  }

  const openGitHubAuth = async () => {
    const redirectUrl = "https://auth.expo.io/@asuki/mobile";
    const authUrl = `https://github.com/login/oauth/authorize?client_id=${clientId}&redirect_uri=${redirectUrl}&scope=user,public_repo`;

    // Open the GitHub authentication URL using WebBrowser
    const result = await WebBrowser.openAuthSessionAsync(authUrl, redirectUrl);

    if (result.type === 'success' && result.url) {
      // Extract the code from the URL
      const codeMatch = result.url.match(/code=([^&]+)/);
      if (codeMatch) {
        const code = codeMatch[1];
        console.log('Code:', code);
        setGithubAuth(false);

        // Fetch and store the token
        fetchToken(code);
      }
    }
  }

  useEffect(() => {
    const originalConsoleWarn = console.warn;
    console.warn = function (message, ...optionalParams) {
      if (typeof message === 'string' && message.includes("Can't open url:")) {
        console.log('WebView Warning:', message);
        setGithubAuth(true);
        openGitHubAuth();
        return;
      }
      originalConsoleWarn.apply(console, [message, ...optionalParams]);
    };
  }, []);

  return (
    <View style={styles.container}>
      <TouchableOpacity style={styles.button} onPress={openGitHubAuth}>
        <Text style={styles.buttonText}>Login with GitHub</Text>
      </TouchableOpacity>
    </View>
  );
}
