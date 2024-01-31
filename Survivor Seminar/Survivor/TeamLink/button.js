import React from 'react';
import { View, Button, StyleSheet } from 'react-native';

const CustomButton = ({ width, height, left, text, top, onPress}) => {
  const buttonStyle = {
    width: width || 200,
    height: height || 40,
    left: left || 100,
    top: top || 0,
  };
  return (
    <View style={[styles.container, buttonStyle]}>
      <Button title={text || "Button"} color="#72009B" onPress={onPress}/>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default CustomButton
