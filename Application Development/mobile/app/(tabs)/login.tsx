import React, { useState, useEffect, useCallback, useContext } from 'react';
import { StyleSheet, Text, View, TextInput, TouchableOpacity, KeyboardAvoidingView, Platform, ImageBackground } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';
import request from '../../constants/request';
import AsyncStorage from "@react-native-async-storage/async-storage"
import UserContext from '../../constants/context';

const useLoginLogic = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);
    const [emailValidated, setemailValidated] = useState(true);
    const [passwordValidated, setPasswordValidated] = useState(true);
    const [error, setError] =  useState<string | null>(null);

    return {
        email,
        setEmail,
        password,
        setPassword,
        showPassword,
        setShowPassword,
        emailValidated,
        setemailValidated,
        passwordValidated,
        setPasswordValidated,
        error,
        setError,
    };
}

export default function Log() {
    const {
        email,
        setEmail,
        password,
        setPassword,
        showPassword,
        setShowPassword,
        emailValidated,
        setemailValidated,
        passwordValidated,
        setPasswordValidated,
        error,
        setError,
    } = useLoginLogic();

    const userCtx = useContext(UserContext)

    useEffect(() => {
        if (error) {
            alert(error)
        }
    }, [error])

    const handleLogin = useCallback(async () => {
       setError(null)
       const response = await request.connection.login({ email, password })

       if (response.error !== null) {
        setError(response.error)
       } else {
            const authToken = response.data.token;
            await AsyncStorage.setItem("authToken", JSON.stringify(response.data))
            userCtx.setUser({
                token: authToken
            })
            setEmail("")
            setPassword("")
            router.replace("/homePage")
        }}, [email, password])

    return (
        <KeyboardAvoidingView
            style={styles.container}
            behavior={Platform.OS === "ios" ? "padding" : "height"}
            keyboardVerticalOffset={Platform.OS === "ios" ? -220 : 0}>
            <ImageBackground source={require('../../assets/images/Background-login.png')} style={styles.backgroundImage} />
            <View style={styles.contentContainer}>
                <ImageBackground source={require('../../assets/images/Logo.png')} style={styles.image} />
                <ImageBackground source={require('../../assets/images/Text-logo.png')} style={styles.textLogo} />
                <Text style={styles.slogan}>Soyez reactif sans même bouger un doigt !</Text>
                <View style={styles.inputContainer}>
                    <Ionicons name="person" size={24} color="white" style={{ paddingRight: 10 }} />
                    <TextInput style={styles.input} placeholder="Email" placeholderTextColor="black" value={email} onChangeText={setEmail} />
                </View>
                <View style={styles.inputContainer}>
                    <Ionicons name="lock-closed" size={24} color="white" style={{ paddingRight: 10 }} />
                    <TextInput style={[styles.input, { paddingRight: 40 }]} placeholder="Mot de Passe" placeholderTextColor="black" value={password} onChangeText={setPassword} secureTextEntry={!showPassword} />
                    <TouchableOpacity onPress={() => setShowPassword(!showPassword)} style={styles.eyeIcon}>
                        <Ionicons name={showPassword ? 'eye-off' : 'eye'} size={24} color="black" />
                    </TouchableOpacity>
                </View>
                <View>
                    <TouchableOpacity style={styles.customButton} onPress={() => handleLogin()}>
                        <Text style={styles.buttonText}>Connexion</Text>
                    </TouchableOpacity>
                    <TouchableOpacity style={styles.customButton} onPress={() => router.replace("/createAccount")}>
                        <Text style={styles.buttonText}>Créer ton compte</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </KeyboardAvoidingView>
    )
}

const COLORS = {
    primary: '#C49D83',
    secondary: '#BDA18A',
    tirtiary: '#E8D5CC',
    orange: '#ffa500',
    light: '#F5EFE6',
    white: '#FFF',
    black: '#000000',
    grey: '#D3D3D3',
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    backgroundImage: {
        width: '100%',
        height: '100%',
        position: 'absolute',
    },
    contentContainer: {
        flex: 1,
        backgroundColor: "rgba(255,255,255,0.0)",
        alignItems: 'center',
        justifyContent: 'center',
    },
    image : {
        width: 100,
        height: 100,
        justifyContent: 'center',
        marginBottom: 10,
    },
    textLogo : {
        width: '100%',
        height: 80,
        justifyContent: 'center',
        marginBottom: 20,
    },
    slogan: {
        fontSize: 18,
        marginBottom: 20,
        textAlign: 'center',
        color: 'white',
    },
    input: {
        width: '95%',
        padding: 10,
        marginVertical: 10,
        backgroundColor: COLORS.grey,
        borderRadius: 5,
        borderColor: COLORS.black,
        borderWidth: 1,
    },
    inputContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        width: '95%',
        position: 'relative'
    },
    eyeIcon: {
        position: 'absolute',
        right: 10,
        top: '50%',
        marginTop: -12
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
