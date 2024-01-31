import React from 'react';
import { NavigationContainer, useNavigation } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

import HomeScreen from './open.js';
import LoginScreen from './login.js';
import HomePage from './HomePage.js';
import User from './user.js';

const backgroundStyle = {
    backgroundColor: '#4d4d4c',
    flex: 1,
};

const Stack = createNativeStackNavigator();

const Navigation = () => {
    return (
      <NavigationContainer>
        <Stack.Navigator>
          <Stack.Screen
              name="Home"
              component={HomeScreen}
              options={{ headerShown: false }}/>
          <Stack.Screen 
              name="Login"
              component={LoginScreen}
              options={{
                  headerStyle: {
                      backgroundColor: backgroundStyle.backgroundColor,
                    },
                    headerTintColor: 'white'
              }}/>
          <Stack.Screen
              name="HomePage"
              component={HomePage}
              options={{ headerShown: false}} />
          <Stack.Screen
              name="UserScreen"
              component={User}
              options={{
                headerStyle: {
                    backgroundColor: backgroundStyle.backgroundColor,
                  },
                  headerTintColor: 'white'
              }}/>
        </Stack.Navigator>
      </NavigationContainer>
    );
  };

  export default Navigation