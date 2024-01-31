import React from 'react';
import { View, Image, StyleSheet, Pressable } from 'react-native';
import { NavigationContainer, useNavigation } from '@react-navigation/native';

const LogoPeople = ({ width, height, left, Top }) => {
  const containerStyle = {
    width: width || 300,
    height: height || 300,
    left: left || 30,
    top: Top || 50,
  };

  const navigation = useNavigation()

  const handleNavigation = () => {
    navigation.navigate('UserScreen');
  };

  return (
    <View style={[styles.container, containerStyle]}>
      <Pressable onPress={handleNavigation}>
      <Image
        source={require('../assets/People.png')}
        style={styles.logo}
      />
      </Pressable>
    </View>
  );
};

const styles = StyleSheet.create({
  logo: {
    top: 720,
    left: 200,
    width: 100,
    height: 80,
    resizeMode: 'contain',
  },
});

export default LogoPeople;
