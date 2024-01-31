import React, { useState, useEffect, useCallback, useContext } from 'react';
import { StyleSheet, Text, View, TextInput, TouchableOpacity, KeyboardAvoidingView, Platform, ActivityIndicator, ImageBackground } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { Modal, Image } from 'react-native'
import * as WebBrowser from "expo-web-browser"
import * as Facebook from 'expo-auth-session/providers/facebook';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Button } from 'react-native-paper';

WebBrowser.maybeCompleteAuthSession();

interface User {
    name: string;
    id: string;
    picture: {
        data: {
            url: string;
        };
    };
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: "rgba(255,255,255,0.0)",
        alignItems: 'center',
        justifyContent: 'center',
    },
});

export default function FacebookLogin() {
    const [userInformation, setUserInformation] = useState(null);
    const redirectUri = 'https://auth.expo.io/asuki/mobile';
    const [request, response, link] = Facebook.useAuthRequest({
        clientId: '707658790781887',
        redirectUri: redirectUri
    });

    useEffect(() => {
        if (response && response.type === 'success') {
            (async () => {
                const userurl = await fetch(`https://graph.facebook.com/me?fields=id,name,picture&access_token=${response.params.access_token}`);
                const userInformation = await userurl.json();
                setUserInformation(userInformation);
            })();
        }
    }, [response]);

    const handleLogin = async () => {
        const result = await link();
        if (result.type !== 'success') {
            alert('An error occurred!');
            return;
        }
    };

    return (
        <View style={styles.container}>
            {userInformation ? <Profile user={userInformation} /> : <Button onPress={handleLogin}>Login with Facebook</Button>}
        </View>
    );
}

function Profile({ user } : { user: User }) {
    return (
      <View>
        <Image source={{ uri: user.picture.data.url }} />
        <Text>{user.name}</Text>
        <Text>{user.id}</Text>
      </View>
    );
}