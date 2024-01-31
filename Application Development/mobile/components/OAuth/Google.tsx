import React, { useState, useEffect, useCallback, useContext } from 'react';
import { StyleSheet, Text, View, TextInput, TouchableOpacity, KeyboardAvoidingView, Platform, ActivityIndicator, ImageBackground } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { Modal } from 'react-native'
import * as WebBrowser from "expo-web-browser"
import * as Google from 'expo-auth-session/providers/google';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Button } from 'react-native-paper';

WebBrowser.maybeCompleteAuthSession();

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: 'rgba(255, 255, 255, 0.0)',
        alignItems: 'center',
        justifyContent: 'center',
    },
});

export default function GoogleLogin() {
    const [userInformation, setUserInformation] = useState(null);
    const [request, response, promptAsync] = Google.useAuthRequest({
        androidClientId:'793068835669-6n3o23i2jep4i445k90qvj6ui7227g0u.apps.googleusercontent.com',
        iosClientId: '793068835669-22i7nn6teo2o040rt0q7fb4j89onuoon.apps.googleusercontent.com',
        redirectUri: 'https://auth.expo.io/@asuki/mobile'
    });

    useEffect(() => {
        if (response?.type === 'success') {
            signInWithGoogleAsync(response.params.access_token);
        }
    }, [response]);

    async function signInWithGoogleAsync(token: string) {
        try {
            const response = await fetch("https://www.googleapis.com/userinfo/v2/me", {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            const userInfoResponse = await response.json();
            await AsyncStorage.setItem("user", JSON.stringify(userInfoResponse));
            setUserInformation(userInfoResponse);
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <View style={styles.container}>
            <Text>{JSON.stringify(userInformation)}</Text>
            <Button onPress={() => promptAsync()}>
                <Ionicons name="logo-google" size={24} color="black" />
                <Text>Sign in with Google</Text>
            </Button>
            <Button onPress={async () => {
                await AsyncStorage.removeItem("user");
                setUserInformation(null);
            }}>
                <Ionicons name="logo-google" size={24} color="black" />
                <Text>Sign out</Text>
            </Button>
        </View>
    );
}
