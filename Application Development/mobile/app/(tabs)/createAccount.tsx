import React, { useState, useEffect, useCallback, useContext } from 'react';
import { StyleSheet, Text, View, TextInput, TouchableOpacity, KeyboardAvoidingView, Platform, ActivityIndicator, ImageBackground } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { Modal } from 'react-native'
import { router } from 'expo-router';
import request from '../../constants/request';
import AsyncStorage from "@react-native-async-storage/async-storage"

export default function Create() {
    const [firstname, setFirstname] = useState("");
    const [password, setPassword] = useState("");
    const [email, setEmail] = useState("");
    const [lastname, setLastname] = useState("");
    const [error, setError] =  useState<string | null>(null);
    const data = AsyncStorage.getItem("data")

    useEffect(() => {
        if (error) {
            alert(error)
        }
    }, [error])

    const handleSignUp = useCallback(async () => {
        setError(null);
        try {
          const registrationResponse = await request.register({ email, password, firstname, lastname });
          if (registrationResponse.error) {
            setError(registrationResponse.error);
            return;
          }
          const emailConfirmationResponse = await request.emailConfirmation(true, email);

          if (emailConfirmationResponse.error) {
            setError(emailConfirmationResponse.error);
            return;
          }
          setEmail("");
          setPassword("");
          setFirstname("");
          setLastname("");
          router.replace("/login");
        } catch (error) {
          setError('Une erreur est survenue, veuillez réessayer en vérifiant vos informations.');
        }
      }, [email, password, firstname, lastname]);

    return (
        <KeyboardAvoidingView
            style={styles.container}
            behavior={Platform.OS === "ios" ? "padding" : "height"}
            keyboardVerticalOffset={Platform.OS === "ios" ? -220 : 20}>
            <View style={styles.container}>
                <ImageBackground source={require('../../assets/images/Background-login.png')} style={{ width: '100%', height: '100%', position: 'absolute' }} />
                <View>
                    <ImageBackground source={require('../../assets/images/Text-logo.png')} style={styles.image} />
                </View>
                <View style={styles.inputContainer}>
                    <Ionicons name="person" size={24} color={COLORS.white} style={{ paddingRight: 10 }} />
                    <TextInput style={styles.input} placeholder="Prénom" placeholderTextColor={COLORS.black} value={firstname} onChangeText={setFirstname} />
                </View>
                <View style={styles.inputContainer}>
                    <Ionicons name="person" size={24} color={COLORS.white} style={{ paddingRight: 10 }} />
                    <TextInput style={styles.input} placeholder="Nom de famille" placeholderTextColor={COLORS.black} value={lastname} onChangeText={setLastname} />
                </View>
                <View style={styles.inputContainer}>
                    <Ionicons name="person" size={24} color={COLORS.white} style={{ paddingRight: 10 }} />
                    <TextInput style={styles.input} placeholder="Email" placeholderTextColor={COLORS.black} value={email} onChangeText={setEmail} />
                </View>
                <View style={styles.inputContainer}>
                    <Ionicons name="lock-closed" size={24} color="white" style={{ paddingRight: 10 }} />
                    <TextInput style={[styles.input, { paddingRight: 40 }]} placeholder="Mot de passe" placeholderTextColor={COLORS.black} value={password} onChangeText={setPassword} secureTextEntry={true} />
                </View>
                <View>
                    <TouchableOpacity style={styles.customButton} onPress={() => handleSignUp()}>
                        <Text style={styles.buttonText}>Create Account</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </KeyboardAvoidingView>
    );
}

const COLORS = {
    primary: '#C49D83',
    secondary: '#BDA18A',
    tirtiary: '#E8D5CC',
    grey: '#D3D3D3',
    light: '#F5EFE6',
    white: '#FFF',
    black: '#000000',
    orange : '#ffa500',
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: "rgba(255,255,255,0.0)",
        alignItems: 'center',
        justifyContent: 'center',
    },
    image : {
        width: '100%',
        height: 80,
        marginBottom: 20,
        right: 190,
    },
    appName: {
        fontSize: 32,
        marginBottom: 10,
        textAlign: 'center',
        color: '#000',
    },
    slogan: {
        fontSize: 18,
        marginBottom: 20,
        textAlign: 'center',
        color: '#000',
    },
    input: {
        width: '95%',
        padding: 10,
        marginVertical: 10,
        backgroundColor: COLORS.grey,
        borderRadius: 5,
        borderColor: COLORS.black,
        borderWidth: 1,
        color: '#000',
    },
    inputContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        width: '95%',
        position: 'relative'
    },
    customButton: {
        backgroundColor: COLORS.grey,
        borderRadius: 5,
        paddingVertical: 12,
        paddingHorizontal: 40,
        marginVertical: 10,
        alignItems: 'center',
        justifyContent: 'center'
    },
    buttonText: {
        color: COLORS.black,
        fontSize: 18,
    }
});