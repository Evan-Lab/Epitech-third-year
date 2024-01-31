import React from 'react';
import { View, StyleSheet } from 'react-native';


const Square = ({width, height, left}) => {
    const containerStyle = {
        width: width || 200,
        height: height || 200,
        left: left || 55
  };
  return (
    <View style={[styles.container, containerStyle]}>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    backgroundColor: 'white',
    borderRadius: 20,
  },
});

export default Square;