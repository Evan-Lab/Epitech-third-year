import { Card } from "react-native-paper"
import { Image, StyleSheet, Text, View } from "react-native"
import FontAwesome from "@expo/vector-icons/FontAwesome"
import { router } from "expo-router"
import { useContext } from "react"

const styles = StyleSheet.create({
  image: {
    width: 60,
    height: 60,
    borderRadius: 150 / 2,
    overflow: "hidden",
    borderWidth: 3,
    borderColor: "black"
  },
  text: {
    fontSize: 18,
  },
})

export default function AreaWidget() {

  return (
    <Card>
      <Card.Title title="C'est quoi une area ?" />
      <Card.Content
        style={{
          flexDirection: "row"
        }}
      >
        <Image style={styles.image} source={ require('../../assets/images/Logo.png') } />
        <View
          style={{
            marginLeft: 20
          }}
        >
          <Text style={{ fontSize: 16, color:'black', marginRight: 40 }}>Fatigué de toujours répété les mêmes actions ? Soyez réactifs sans même bouger un doigt. Créez vos propres actions ou réactions ou connectez vous seulement à ceux déjà existantes !</Text>
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
              router.replace("/area")
            }}
          />
        </View>
      </Card.Content>
    </Card>
  )
}