import FontAwesome from '@expo/vector-icons/FontAwesome';
import { DarkTheme, DefaultTheme, ThemeProvider } from '@react-navigation/native';
import { useFonts } from 'expo-font';
import { SplashScreen, Stack, router } from 'expo-router';
import { useEffect } from 'react';
import { useColorScheme } from 'react-native';
import { useState } from 'react';
import  UserContext  from '../constants/context';
import { StorageLogin } from '../constants/storage';
import AsyncStorage from "@react-native-async-storage/async-storage"

export {
  // Catch any errors thrown by the Layout component.
  ErrorBoundary,
} from 'expo-router';

export const unstable_settings = {
  // Ensure that reloading on `/modal` keeps a back button present.
  initialRouteName: '(tabs)',
};

// Prevent the splash screen from auto-hiding before asset loading is complete.
SplashScreen.preventAutoHideAsync();

export default function RootLayout() {
  const [loaded, error] = useFonts({
    SpaceMono: require('../assets/fonts/SpaceMono-Regular.ttf'),
    ...FontAwesome.font,
  });

  // Expo Router uses Error Boundaries to catch errors in the navigation tree.
  useEffect(() => {
    if (error) throw error;
  }, [error]);

  useEffect(() => {
    if (loaded) {
      SplashScreen.hideAsync();
    }
  }, [loaded]);

  if (!loaded) {
    return null;
  }

  return <RootLayoutNav />;
}

function RootLayoutNav() {
  const colorScheme = useColorScheme();
  const [user, setUser] = useState<StorageLogin | null | undefined>(undefined)

  useEffect(() => {
    (async () => {
    if (user === undefined ) {
      try {
        const user = await AsyncStorage.getItem("user")
        if (user === null) {
          setUser(null)
        } else {
          setUser(JSON.parse(user))
        }
      } catch (e) {
        setUser(null)
      }}})()
    if (user === null) {
      router.replace("/login")
    }
  }, [user])

  return (
    <UserContext.Provider value={{ user, setUser }}>
    <ThemeProvider value={colorScheme === 'dark' ? DarkTheme : DefaultTheme}>
      <Stack>
        <Stack.Screen name="(tabs)" options={{ headerShown: false }} />
        <Stack.Screen name="modal" options={{ presentation: 'modal' }} />
      </Stack>
    </ThemeProvider>
    </UserContext.Provider>
  )
}
