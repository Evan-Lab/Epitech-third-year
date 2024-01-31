import React from 'react';
import { View, Image, StyleSheet } from 'react-native';

const Logo = ({ logoPath }) => {
  return (
    <View style={styles.header}>
      <Image
        source={require('../assets/default_logo.png')}
        style={styles.logo}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  header: {
    height: 115,
    right: 2,
    justifyContent: 'center',
    alignItems: 'center',
  },
  logo: {
    width: 200,
    height: 80,
    resizeMode: 'contain',
  },
});

export default Logo;
