import React from 'react';
import { View, Image, StyleSheet } from 'react-native';

const LogoConnection = () => {
  return (
    <View style={styles.header}>
      <Image
        source={require('../assets/logo_connexion.png')}
        style={styles.logoConnection}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  header: {
    height: 400,
    left: 10,
    justifyContent: 'center',
    alignItems: 'center',
  },
  logoConnection: {
    width: 1200,
    height: 480,
    resizeMode: 'contain',
  },
});

export default LogoConnection;