import React from 'react';
import { View, Image, StyleSheet } from 'react-native';

const LogoPeople = ({ width, height, left, Top }) => {
  const containerStyle = {
    width: width || 300,
    height: height || 300,
    left: left || 20,
    top: Top || 410,
  };

  return (
    <View style={[styles.container, containerStyle]}>
      <Image
        source={require('../assets/People.png')}
        style={styles.logo}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  logo: {
    width: 100,
    height: 80,
    resizeMode: 'contain',
  },
});

export default LogoPeople;
