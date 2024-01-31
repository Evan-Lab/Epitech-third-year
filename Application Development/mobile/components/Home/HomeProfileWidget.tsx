import { Card } from "react-native-paper"
import { Image, StyleSheet, Text, View } from "react-native"
import FontAwesome from "@expo/vector-icons/FontAwesome"
import { router } from "expo-router"
import { useContext } from "react"
import UserContext from "../../constants/context"
import { useEffect, useState } from "react"
import request from "../../constants/request"
import AsyncStorage from "@react-native-async-storage/async-storage"

const styles = StyleSheet.create({
  image: {
    width: 150,
    height: 150,
    borderRadius: 150 / 2,
    overflow: "hidden",
    borderWidth: 3,
    borderColor: "black"
  },
  text: {
    fontSize: 20,
  },
  smallText: {
    fontSize: 14,
    marginRight: 40
  },
})

export default function HomeProfileWidget() {
  const userCtx = useContext(UserContext)
  const [firstname, setFirstname] = useState("");
  const [email, setEmail] = useState("");
  const [lastname, setLastname] = useState("");
  const [error, setError] =  useState<string | null>(null);
  const data = AsyncStorage.getItem("data")

  const updateProfile = async () => {
    setError(null);
    try {
      const response = await request.infoProfile(userCtx.user!.token);
      if (response.error !== null) {
        setError(response.error);
      } else {
        setFirstname(response.data.firstname);
        setLastname(response.data.lastname);
        setEmail(response.data.email);
      }
    } catch (error) {
      console.error('Update Profile Error:', error);
    }
  }

  useEffect(() => {
    updateProfile();
  }, [error])

  return (
    <Card>
      <Card.Title title="Profile" />
      <Card.Content
        style={{
          flexDirection: "row"
        }}
      >
        <Image style={styles.image} source={{
            uri: "https://www.w3schools.com/howto/img_avatar.png"
          }} />
        <View
          style={{
            marginLeft: 20
          }}
        >
          <Text style={styles.text}> {firstname} {lastname} </Text>
          <View style={{ height: 1, backgroundColor: "black" }} />
          <View style={{ height: 15 }} />
          <Text style={styles.smallText}> {email} </Text>
          <View style={{ height: 30 }} />
          <Text style={styles.smallText}> Membre depuis {new Date().getFullYear()} </Text>
        </View>
        <View
          style={{
            position: "absolute",
            right: 0,
            bottom: 0
          }}
        >
          <FontAwesome
            size={28}
            style={{ marginRight: 5 }}
            name="angle-double-down"
            onPress={() => {
              router.replace("/profileCard")
            }}
          />
        </View>
      </Card.Content>
    </Card>
  )
}
