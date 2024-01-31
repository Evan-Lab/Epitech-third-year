import { useState } from "react"
import { View, Text, StyleSheet, ImageBackground } from "react-native"
import { Button } from "react-native-paper"
import AreaList from "../../components/Area/AreaList"
import CreatArea from "../../components/Area/CreateArea"
import AreaListPage from "../../components/Area/AreaListPage"

enum AreaMode {
  AreaCreate = "areaCreate",
  Area = "area"
}

const styles = StyleSheet.create({
  container: {
    rowGap: 20,
    flex: 1,
    alignItems: "center",
    justifyContent: "center"
  },
  buttonContainer: {
    flexDirection: "row",
    justifyContent: "center",
    paddingVertical: 10
  },
  buttonWrapper: {
    marginHorizontal: 10
  },
  button: {
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 5,
    backgroundColor: "grey"
  },
  selectedButton: {
    backgroundColor: 'orange'
  },
  buttonText: {
    color: "white",
    fontSize: 18,
    fontWeight: "bold"
  }
})

export default function AreaPage() {
  const [mode, setMode] = useState<AreaMode>(AreaMode.AreaCreate)

  return (
    <ImageBackground source={require('../../assets/images/Background-login.png')} style={{ width: '100%', height: '100%', position: 'absolute' }} >
      <View style={styles.container}>
        <View style={styles.buttonContainer}>
          <Button
            mode="contained"
            onPress={() => setMode(AreaMode.Area)}
            style={[styles.button, mode === AreaMode.Area ? styles.selectedButton : null]}
          >
            <Text style={styles.buttonText}>{("Area")}</Text>
          </Button>
          <View style={styles.buttonWrapper}>
            <Button
              mode="contained"
              onPress={() => setMode(AreaMode.AreaCreate)}
              style={[styles.button, mode === AreaMode.AreaCreate ? styles.selectedButton : null]}
            >
              <Text style={styles.buttonText}>{("Creér ton Area")}</Text>
            </Button>
          </View>
        </View>
        {mode === AreaMode.Area && <AreaListPage />}
        {mode === AreaMode.AreaCreate && <CreatArea />}

      </View>
    </ImageBackground>
  )
}