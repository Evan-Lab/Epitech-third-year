import React from 'react';
import { View, StyleSheet } from 'react-native';
import { NavigationContainer, useNavigation } from '@react-navigation/native';

import LogoConnection from './HomeLogo/logoConnection.js';
import Square from './Utils/square.js';
import CustomButton from './button.js'

import LoginScreen from './login.js';

const backgroundStyle = {
    backgroundColor: '#4d4d4c',
    flex: 1,
};

const HomeScreen = () => {
    const navigation = useNavigation();

    const handleLogin = () => {
        navigation.navigate('Login');
    };
    return (
        <View style={backgroundStyle}>
            <LogoConnection />
            <Square
                width={300}
                height={300}
                left={55}
            />
            <CustomButton
                width={200}
                height={300}
                left={100}
                top={-350}
                text="Login"
                onPress={handleLogin}
            />
            <CustomButton
                width={200}
                height={300}
                left={100}
                top={-550}
                text="Register"
            />
        </View>
    );
}

export default HomeScreen;