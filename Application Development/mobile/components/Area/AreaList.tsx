import { Card, Searchbar } from "react-native-paper"
import { Image, StyleSheet, Text, View, TextInput } from "react-native"
import FontAwesome from "@expo/vector-icons/FontAwesome"
import { router } from "expo-router"
import { useContext } from "react"
import { ScrollView } from "react-native-gesture-handler"
import { useState } from "react"


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

export default function AreaList() {
  const MAX_TEXT_LENGTH = 100;
  const longText =
  "Fatigué de toujours répéter les mêmes actions ? Soyez réactifs sans même bouger un doigt. Créez vos propres actions ou réactions ou connectez vous seulement à ceux déjà existantes !";

  const shortText =
    longText.length > MAX_TEXT_LENGTH
      ? `${longText.substring(0, MAX_TEXT_LENGTH)}...`
      : longText;

  const [searchKeyword, setSearchKeyword] = useState("");
  const [query, setQuery] = useState<string | null>(null)

  const filteredCards = [
        "GithubDiscord",
        "DeezerFacebook",
        "YoutubeDiscord",
        "GithubDeezer",
        "FacebookYoutube",
        "YoutubeCloud",
        "YoutubeDeezer",
        "CloudDiscord",
        "GithubYoutube",
        "GithubFacebook",
        "GithubCloud",
        "DiscordFacebook",
        "CloudDeezer",
        "CloudFacebook",
        "DiscordDeezer"
  ].filter((cardTitle) => cardTitle.toLowerCase().includes(searchKeyword.toLowerCase()));

  return (
    <ScrollView>
    <Searchbar
        style={{
          borderWidth: 1,
          marginBottom: 20,
          paddingLeft: 10,
        }}
        placeholder="Search..."
        onChangeText={(text) => setSearchKeyword(text)}
        value={searchKeyword}
      />
      {filteredCards.map((cardTitle) => (
        <Card style={{ marginBottom: 20 }} key={cardTitle}>
         <Card.Title title={cardTitle} />
         <Card.Content
           style={{
             flexDirection: "row",
           }}
         >
           <View
             style={{
               marginLeft: 20,
             }}
           >
             <Text style={{ fontSize: 16, color: "black", marginRight: 40 }}>
               {shortText}
             </Text>
           </View>
           <View
             style={{
               position: "absolute",
               right: 0,
               bottom: 0,
             }}
           >
             <FontAwesome
               size={28}
               style={{ marginRight: 5 }}
               name="angle-double-down"
               onPress={() => {
                 router.replace("/area");
               }}
             />
           </View>
         </Card.Content>
       </Card>
     ))}
   </ScrollView>
  )
}
