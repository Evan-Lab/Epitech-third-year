import { StyleSheet, View, ImageBackground, ScrollView } from "react-native"
import { Card, Searchbar } from "react-native-paper"
import { router } from "expo-router"
import HomeProfileWidget from "../../components/Home/HomeProfileWidget"
import AreaWidget from "../../components/Home/AreaWidget"
import { useState } from "react"
import HomePageAreaList from "../../components/Area/HomePageAreaList"

const styles = StyleSheet.create({
  container: {
    marginVertical: 10,
    marginHorizontal: 10,
    rowGap: 30,
  },
  card: {
    maxHeight: "40%",
  },
  cardContent: {
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center"
  },
  cardCover: {
    width: "100%",
    height: "85%"
  },
})

const backgroundImage = require("../../assets/images/Background-login.png")

export default function HomePage() {
    const [searchKeyword, setSearchKeyword] = useState("");

    return (
      <ImageBackground
        source={backgroundImage}
        style={{ width: "100%", height: "100%", position: "absolute" }}
      >
      <ScrollView>
        <View style={styles.container}>
        <AreaWidget />
        <HomeProfileWidget />
        </View>
        </ScrollView>
      </ImageBackground>
  );
}