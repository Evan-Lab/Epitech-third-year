import React , { useState } from 'react';
import { View, StyleSheet, Text, TextInput, Alert } from 'react-native';
import { NavigationContainer, useNavigation } from '@react-navigation/native';
// import AsyncStorage from '@react-native-async-storage/async-storage';

import Logo from './HomeLogo/logo.js';
import Square from './Utils/square.js';
import CustomButton from './button.js';

const backgroundStyle = {
    backgroundColor: '#4d4d4c',
    flex: 1,
};

const HandleLogin = ({email, password, navigation}) => {
    const token = 'gbow2QTCOc-rPjcWkNnsKw_a3vuw1DdE'

    // AsyncStorage.setItem('authToken', token)

    fetch('https://masurao.fr/api/employees/login', {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            'X-Group-Authorization': token,
        },
        body: JSON.stringify({
            email: email,
            password: password,
        }),
    })
    .then(response => response.json())
    .then(json => {
        // console.log("Access Token: ", json['access_token'])
        // AsyncStorage.setItem('accessToken', json['access_token'])
        // return json.movies;
        navigation.navigate("HomePage")
    })
    .catch(error => {
      console.error(error);
    })

}

const LoginScreen = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigation = useNavigation();

    return (
        <View style={backgroundStyle}>
            <Logo />
            <Square
                width={300}
                height={650}
                left={55}
            />
            <Text style={{ fontSize: 40, height: 60, top: -600, left: 110 }}>Connexion</Text>
            <TextInput
                style={{ height: 40, width: 200, top: -500, left: 100, borderColor: 'black', borderWidth: 1 }}
                placeholder="Email"
                value={email}
                onChangeText={text => setEmail(text)}/>
            <TextInput
                style={{ height: 40, width: 200, top: -400, left: 100, borderColor: 'black', borderWidth: 1 }}
                secureTextEntry={true}
                placeholder="Password"
                value={password}
                onChangeText={text => setPassword(text)}/>
            <CustomButton
                width={200}
                height={300}
                left={100}
                top={-450}
                text="Login"
                onPress={() => HandleLogin({email, password, navigation})}
            />
        </View>
    );
}

export default LoginScreen;