import FontAwesome from '@expo/vector-icons/FontAwesome';
import { Link, Tabs } from 'expo-router';
import { Pressable, useColorScheme } from 'react-native';

import Colors from '../../constants/Colors';

/**
 * You can explore the built-in icon families and icons on the web at https://icons.expo.fyi/
 */
function TabBarIcon(props: {
  name: React.ComponentProps<typeof FontAwesome>['name'];
  color: string;
}) {
  return <FontAwesome size={28} style={{ marginBottom: -3 }} {...props} />;
}

export default function TabLayout() {
  const colorScheme = useColorScheme();

  return (
    <Tabs
      screenOptions={{
        tabBarActiveTintColor: Colors[colorScheme ?? 'light'].tint,
      }}>
      <Tabs.Screen
        name="login"
        options={{
          tabBarIcon: ({ color }) => <TabBarIcon name="code" color={color} />,
          href: null,
          headerShown: false,
          tabBarStyle: {
            display: "none"
          }
        }}
      />
      <Tabs.Screen
        name="createAccount"
        options={{
          tabBarIcon: ({ color }) => <TabBarIcon name="code" color={color} />,
          href: null,
          headerShown: false,
          tabBarStyle: {
            display: "none"
          }
        }}
      />
        <Tabs.Screen
        name="index"
        options={{
          tabBarIcon: ({ color }) => <TabBarIcon name="code" color={color} />,
          href: null,
          headerShown: false,
          tabBarStyle: {
            display: "none"
          }
        }}
      />
      <Tabs.Screen
        name="homePage"
        options={{
          tabBarIcon: ({ color }) => <TabBarIcon name="code" color={color} />,
        }}
      />
      <Tabs.Screen
        name="area"
        options={{
          tabBarIcon: ({ color }) => <TabBarIcon name="code" color={color} />,
        }}
      />
      <Tabs.Screen
        name="profileCard"
        options={{
          tabBarIcon: ({ color }) => <TabBarIcon name="code" color={color} />,
        }}
      />
    </Tabs>
  );
}
