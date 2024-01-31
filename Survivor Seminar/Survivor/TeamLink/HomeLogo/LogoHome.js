import React from 'react';
import { View, Image, StyleSheet, Pressable } from 'react-native';
import { NavigationContainer, useNavigation } from '@react-navigation/native';

const LogoHome = ({ logoPath }) => {
  const navigation = useNavigation()

  const handleNavigation = () => {
    navigation.navigate('HomePage');
  };
  return (
    <View style={styles.header}>
      <Pressable onPress={handleNavigation}>
        <Image
          source={require('../assets/Home.png')}
          style={styles.logo}
        />
      </Pressable>
    </View>
  );
};

const styles = StyleSheet.create({
  header: {
    height: 115,
    right: 100,
    top: 440,
    justifyContent: 'center',
    alignItems: 'center',
  },
  logo: {
    width: 110,
    height: 80,
    resizeMode: 'contain',
  },
});

export default LogoHome;
