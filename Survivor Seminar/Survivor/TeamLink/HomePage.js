import React from 'react';
import { View, StyleSheet, Text } from 'react-native';
import { Card } from 'react-native-paper';

import LogoPeople from './HomeLogo/LogoPeople';
import LogoHome from './HomeLogo/LogoHome';
import Logo from './HomeLogo/logo';

const backgroundStyle = {
    backgroundColor: '#4d4d4c',
    flex: 1,
};

const cardStyle = {
    width: 200,
    height: 200,
    top: -300,
};

const HomePage = () => {
    return (
        <View style={backgroundStyle}>
            <LogoPeople />
            <LogoHome />
            <Card style={cardStyle}>
                <Card.Title title="La photo du jour" />
                <Card.Cover source={{ uri: 'https://picsum.photos/700' }} />
            </Card>
        </View>
    );
}

export default HomePage;
