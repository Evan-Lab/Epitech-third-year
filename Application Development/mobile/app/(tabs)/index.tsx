import { ActivityIndicator } from "react-native"
import { Redirect } from "expo-router"
import { ImageBackground } from "react-native"
import { useContext } from "react"
import  UserContext from "../../constants/context"

const backgroundImage = require("../../assets/images/Background-login.png")


export default function Index() {
  const userCtx = useContext(UserContext)
  return (
    <ImageBackground source={backgroundImage} style={{ width: "100%", height: "100%", position: "absolute" }}>
      {userCtx.user === null && <Redirect href={"/login"} />}
      {userCtx.user && <Redirect href={"/homePage"} />}
    </ImageBackground>
  )
}