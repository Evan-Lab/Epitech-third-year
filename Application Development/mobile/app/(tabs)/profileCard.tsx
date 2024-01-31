import { Image, StyleSheet, View, ScrollView, ImageBackground } from "react-native"
import { Button, Card, Text } from "react-native-paper"
import { router } from "expo-router"
import FontAwesome from "@expo/vector-icons/FontAwesome"
import { Ionicons } from "@expo/vector-icons"
import { useEffect, useState } from "react"
import { TextInput, TouchableOpacity } from "react-native-gesture-handler"
import AsyncStorage from "@react-native-async-storage/async-storage"
import { useContext } from "react"
import UserContext from "../../constants/context"
import { useCallback } from "react"
import request from "../../constants/request"
import { response } from "express"


const styles = StyleSheet.create({
  card: {
    margin: 10,
    backgroundImage: "../../assets/images/Background-login.png",
    borderRadius: 20,
    justifyContent: "center",
    flexDirection: "column",
  },
  cardContent: {
    flexDirection: "column",
    alignItems: "center",
  },
  image: {
    width: 150,
    height: 150,
    borderRadius: 150 / 2,
    overflow: "hidden",
    borderWidth: 3,
    borderColor: "black",
    marginBottom: 10
  },
  bottomBox: {
    flexDirection: "row",
  },
  iconImage: {
    width: 80,
    height: 80,
    borderWidth: 3,
    borderColor: "black",
    borderRadius: 150 / 2,
    margin: 10
  },
  editDescriptionContainer: {
    flexDirection: 'column',
    alignItems: 'center',
    marginBottom: '5%',
  },
  editDescriptionInput: {
    fontSize: 16,
    color: 'white',
    borderWidth: 1,
    borderColor: 'white',
    borderRadius: 8,
    paddingHorizontal: 10,
    paddingVertical: 5,
    marginTop: 10,
    maxHeight: 100,
  },
})

export default function ProfileCard() {
  const MAX_DESCRIPTION_LENGTH = 150;
  const [isEditMode, setIsEditMode] = useState(false);
  const [description, setDescription] = useState(
    'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec euismod, nisl eget aliquam ultricies, nunc nisl aliquet nunc, vitae aliquam nisl nunc eu nisl. Donec euismod, nisl eget aliquam ultricies, nunc nisl aliquet nunc, vitae aliquam nisl nunc eu nisl.'
  );
  const [error, setError] = useState<string | null>(null);
  const userCtx = useContext(UserContext)
  const [firstname, setFirstname] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [lastname, setLastname] = useState("");
  const [date, setDate] = useState("");
  const [userId, setUserId] = useState("");
  const [idUser, setIdUser] = useState<number>(0);
  const [Id, setId] = useState("");
  const [isChangingPassword, setIsChangingPassword] = useState(false);
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [passwordError, setPasswordError] = useState("");

  const handleEditClick = () => {
    setIsEditMode(true);
  };

  const handleSaveClick = () => {
    setIsEditMode(false);
  };

  const handleDescriptionChange = (text: string) => {
    if (text.length <= MAX_DESCRIPTION_LENGTH) {
      setDescription(text);
    }
  };

  const handleLogout = useCallback(async () => {
    await AsyncStorage.removeItem("user", () => {
      userCtx.setUser(null)
      router.replace("/login")
    })
  }, [userCtx.user])


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
        setDate(response.data.dateCreation);
        setUserId(response.data.id);
      }
    } catch (error) {
      console.error('Update Profile Error:', error);
    }
  }


  const handleGiveAdminClick = async () => {
    try {
      const response = await request.giveAdmin({ idUser });
      if (response.error === null) {
        setError(null);
        if (response.data) {
          setIdUser(response.data.idUser);
        }
      } else {
        setError(response.error);
      }
    } catch (error) {
      console.error("Erreur dans la gestion des droits admin:", error);
    }
  };

  const handleDeleteAdminClick = async () => {
    try {
      const response = await request.deleteAdmin({ idUser });
      if (response.error === null) {
        setError(null);
        if (response.data) {
          setIdUser(response.data.idUser);
        }
      } else {
        setError(response.error);
      }
    } catch (error) {
      console.error("Erreur dans la supression des droits admin:", error);
    }
  };

  const handleChangePassword = async () => {
    try {
      if (newPassword !== confirmPassword) {
        setPasswordError("Passwords do not match.");
        return;
      }

      const response = await request.changePassword({
        Id: userId,
        Password: newPassword,
      });
      if (response.error === null) {
        setError(null);
        if (response.data) {
          setId(response.data.Id);
          setPassword(response.data.Password);
        }
      } else if (response.error === "Unauthorized") {
        setError("Unauthorized: Please log in or check your credentials.");
      } else {
        setError(response.error);
      }
    } catch (error) {
      console.error("Error changing password:", error);
    }
  };

    useEffect(() => {
      updateProfile();
    }, [error])

  return (
    <ScrollView>
      <ImageBackground source={require('../../assets/images/Background-login.png')} style={{ width: '100%', height: '100%', position: 'absolute' }} />
      <View style={{ alignItems: "center" }}>
        <Image
          style={styles.image}
          source={{
            uri: "https://www.w3schools.com/howto/img_avatar.png"
          }}
        />
      </View>
      <Card.Content style={styles.cardContent}>
        <Text style={{ fontSize: 20, color: 'white' }}> {firstname} {lastname} </Text>
        <Text style={{ fontSize: 16, color: 'white' }}>
          <FontAwesome name="envelope" size={16} /> {email}
        </Text>
        <Text style={{ fontSize: 16, color: 'white' }}>
          <FontAwesome name="suitcase" size={16} /> {date.slice(0, 10)}
        </Text>
        <Text style={{ fontSize: 16, color: 'white' }}>
          <FontAwesome name="id-card" size={16} /> {userId} (Ton User ID)
        </Text>
      </Card.Content>
      <Card.Content style={styles.cardContent}>
        {isEditMode ? (
          <View style={styles.editDescriptionContainer}>
            <TextInput
              style={styles.editDescriptionInput}
              value={description}
              onChangeText={handleDescriptionChange}
              multiline
              maxLength={MAX_DESCRIPTION_LENGTH}
            />
            <Button onPress={handleSaveClick} buttonColor="grey">
              <Text>Sauvegarde ta Description</Text>
            </Button>
          </View>
        ) : (
          <View style={{ flexDirection: 'row', justifyContent: 'space-between' }}>
            <Text style={{ fontSize: 16, color: 'white' }}>{description}</Text>
            <TouchableOpacity onPress={handleEditClick}>
              <Ionicons name="create-outline" size={24} color="white" />
            </TouchableOpacity>
          </View>
        )}
      </Card.Content>
      <ScrollView horizontal={true} style={styles.bottomBox}>
        <Image
          style={styles.iconImage}
          source={
            require('../../assets/images/Youtube.png')
          }
        />
        <Image
          style={styles.iconImage}
          source={
            require('../../assets/images/Spotify.png')
          }
        />
        <Image
          style={styles.iconImage}
          source={
            require('../../assets/images/time.png')
          }
        />
        <Image
          style={styles.iconImage}
          source={
            require('../../assets/images/Github.png')
          }
        />
        <Image
          style={styles.iconImage}
          source={
            require('../../assets/images/meteo.png')
          }
        />
        <Image
          style={styles.iconImage}
          source={
            require('../../assets/images/Google.png')
          }
        />
      </ScrollView>
      <Card.Actions style={styles.cardContent}>
        {isChangingPassword ? (
          <View>
            <TextInput
              style={styles.editDescriptionInput}
              placeholder="Nouveau mot de passe"
              placeholderTextColor="white"
              secureTextEntry={true}
              value={newPassword}
              onChangeText={setNewPassword}
            />
            <TextInput
              style={styles.editDescriptionInput}
              placeholder="Confirme ton mot de passe"
              placeholderTextColor="white"
              secureTextEntry={true}
              value={confirmPassword}
              onChangeText={setConfirmPassword}
            />
            {passwordError && <Text style={{ color: "red" }}>{passwordError}</Text>}
            <Button buttonColor="grey" onPress={handleChangePassword}>
              <Text>Sauvegarde ton mot de passe</Text>
            </Button>
          </View>
        ) : (
          <Button buttonColor="grey" onPress={() => setIsChangingPassword(true)}>
            <Text>Change ton mot de passe</Text>
          </Button>
        )}
      </Card.Actions>
      <Card.Actions style={styles.cardContent}>
        <Button buttonColor="grey" onPress={handleGiveAdminClick}>
          <Text>Donne l'admin</Text>
        </Button>
      </Card.Actions>
      <Card.Actions style={styles.cardContent}>
        <Button buttonColor="grey" onPress={handleDeleteAdminClick}>
          <Text>Supprime le droit admin</Text>
        </Button>
      </Card.Actions>
      <Card.Actions style={styles.cardContent}>
        <Ionicons name="log-out-outline" size={24} color="white" justifyContent="flex-end" onPress={handleLogout} />
      </Card.Actions>
    </ScrollView>
  )
}
