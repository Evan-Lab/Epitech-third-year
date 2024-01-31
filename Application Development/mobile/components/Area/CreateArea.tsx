import * as React from 'react';
import { View, StyleSheet, Text, TouchableOpacity } from 'react-native';
import { ScrollView } from 'react-native-gesture-handler';
import { Button, Menu, Provider, Card } from 'react-native-paper';
import { Ionicons } from "@expo/vector-icons"
import { TextInput } from 'react-native-gesture-handler';
import { useState, useEffect } from 'react';
import AddTracktoPlaylist from './Spotify/AddTracktoPlaylist';
import FollowArtist from './Spotify/FollowArtist';
import FollowPlaylist from './Spotify/FollowPlaylist';
import NewFollower from './Spotify/NewFollower';
import NewPlaylist from './Spotify/NewPlaylist';
import NewTrack from './Spotify/NewTrack';
import CreateRepository from './Github/Create';
import PullRequest from './Github/Pull';
import PushRequest from './Github/Push';
import PullRequestClose from './Github/PullRequestClose';
import NewBranch from './Github/NewBranch';
import NewCollaborator from './Github/NewCollaborator';
import DeleteBranch from './Github/DeleteBranch';
import AddVideoPlaylist from './Youtube/AddVideoPlaylist';
import MailWeather from './Weather/Mail';
import TimeWeather from './Weather/Time';
import Subscribe from './Youtube/Subscribe';
import NewYTFollower from './Youtube/NewFollower';
import NewVideo from './Youtube/NewVideo';
import WorldTime from './WorldTime/WorldTime';
import request from '../../constants/request';
import { ApiArea } from '../../constants/api';
import { router } from 'expo-router';
import MenuItem from 'react-native-paper/lib/typescript/components/Menu/MenuItem';


export default function CreateArea() {
  const [visible, setVisible] = React.useState(false);
  const [selectedAppForAction, setSelectedAppForAction] = useState('')
  const [selectedAppForReaction, setSelectedAppForReaction] = useState('')
  const [visibleActions, setVisibleActions] = useState(false);
  const [selectAction, setSelectAction] = useState('')
  const [selectReaction, setSelectReaction] = useState('')
  const [name, setName] = useState('')
  const [userId, setUserId] = useState<number>(0)
  const [actionId, setActionId] = useState<number>(0)
  const [reactionId, setReactionId] = useState<number>(0)
  const [paramAction, setParamAction] = useState<{
    hour?: number;
    minute?: number;
    url?: string;
    city?: string;
    name?: string;
    day?: number;
    month?: number;
    year?: number;
    hourParam?: number;
    minuteParam?: number;
  }>({});
  const [paramReaction, setParamReaction] = useState<{
    urlVideo?: string;
    urlPlaylist?: string;
    email?: string;
    urlTrack?: string;
    url?: string;
  }>({});
  const [area, setArea] = useState<ApiArea[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchAreas();
  }, []);

  const fetchAreas = async () => {
    try {
      const response = await request.areaList();
      if (response.error === null) {
        setError(null);
        if (response.data) {
          setArea(response.data);
        }
      }
    } catch (error) {
      console.error('AreaList Error:', error);
    }
  };

  const openMenu = () => setVisible(true);
  const closeMenu = () => setVisible(false);

    const handleAppSelect = (app: string) => {
    if (!selectedAppForAction) {
      setSelectedAppForAction(app);
    } else {
      setSelectedAppForReaction(app);
      closeMenu();
    }
  };

  const handleActionSelect = (action: string) => {
    setSelectAction(action);
    closeMenu();
  };

  const handleReactionSelect = (action: string) => {
    setSelectReaction(action);
    closeMenu();
  };

  const changeActionApp = () => {
    setSelectedAppForAction('');
    openMenu();
  };

  const changeReactionApp = () => {
    setSelectedAppForReaction('');
    openMenu();
  };

  const handleCreateClick = async () => {
    if (selectedAppForAction && selectedAppForReaction) {
      try {
        let actionId = 0;
        let reactionId = 0;

        if (selectedAppForAction === 'Meteo' && selectAction === 'Choisis ton temps') {
          actionId = 1;
        }
        if (selectedAppForAction === 'Github' && selectAction === 'Pull') {
          actionId = 7;
        }
        if (selectedAppForAction === 'Github' && selectAction === 'Push') {
          actionId = 8;
        }
        if (selectedAppForAction === 'Github' && selectAction === 'Create') {
          actionId = 6;
        }
        if (selectedAppForAction === 'Github' && selectAction === 'PullRequestClose') {
          actionId = 5;
        }
        if (selectedAppForAction === 'Github' && selectAction === 'NewBranch') {
          actionId = 3;
        }
        if (selectedAppForAction === 'Github' && selectAction === 'NewCollaborator') {
          actionId = 4;
        }
        if (selectedAppForAction === 'Github' && selectAction === 'DeleteBranch') {
          actionId = 2;
        }
        if (selectedAppForAction === 'Spotify' && selectAction === 'Track') {
          actionId = 11;
        }
        if (selectedAppForAction === 'Spotify' && selectAction === 'Playlist') {
          actionId = 10;
        }
        if (selectedAppForAction === 'Spotify' && selectAction === 'Follower') {
          actionId = 9;
        }
        if (selectedAppForAction === 'WorldTime' && selectAction === 'Choisis ton heure') {
          actionId = 12;
        }
        if (selectedAppForAction === 'Youtube' && selectAction === 'New Follower') {
          actionId = 13;
        }
        if (selectedAppForAction === 'Youtube' && selectAction === 'New Video') {
          actionId = 15;
        }
        if (selectedAppForReaction === 'Mail' && selectReaction === 'Remplis ton email') {
          reactionId = 1;
        }
        if (selectedAppForReaction === 'Youtube' && selectReaction === 'Subscribe') {
          reactionId = 5;
        }
        if (selectedAppForReaction === 'Youtube' && selectReaction === 'Add to playlist') {
          reactionId = 6;
        }
        if (selectedAppForReaction === 'Spotify' && selectReaction === 'Add song to playlist') {
          reactionId = 2;
        }
        if (selectedAppForReaction === 'Spotify' && selectReaction === 'FollowArtist') {
          reactionId = 4;
        }
        if (selectedAppForReaction === 'Spotify' && selectReaction === 'FollowePlaylist') {
          reactionId = 3;
        }

        const formattedParamAction = JSON.stringify(paramAction);
        const formattedParamReaction = JSON.stringify(paramReaction);

        const response = await request.createArea({
          name,
          userId,
          actionId,
          reactionId,
          paramAction: formattedParamAction,
          paramReaction: formattedParamReaction,
        });
        fetchAreas();
      } catch (error) {
        console.error('CreateArea Error:', error);
      }
    }
  };


  const MAX_DESCRIPTION_LENGTH = 150;
  const [isEditMode, setIsEditMode] = useState(false);
  const [description, setDescription] = useState(
    'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec euismod, nisl eget aliquam ultricies, nunc nisl aliquet nunc, vitae aliquam nisl nunc eu nisl. Donec euismod, nisl eget aliquam ultricies, nunc nisl aliquet nunc, vitae aliquam nisl nunc eu nisl.'
  );

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

  return (
    <Provider>
      <ScrollView style={{ flexDirection: 'column', marginLeft: 20 }}>
        <Card style={styles.card}>
        <Card.Content>
            <Menu
              visible={visible}
              onDismiss={closeMenu}
              anchor={<Button onPress={openMenu} textColor='orange'>Selecte une application</Button>}
            >
              <Menu.Item onPress={() => handleAppSelect('WorldTime')} title="WorldTime" />
              <Menu.Item onPress={() => handleAppSelect('Spotify')} title="Spotify" />
              <Menu.Item onPress={() => handleAppSelect('Mail')} title="Mail" />
              <Menu.Item onPress={() => handleAppSelect('Github')} title="Github" />
              <Menu.Item onPress={() => handleAppSelect('Youtube')} title="Youtube" />
              <Menu.Item onPress={() => handleAppSelect('Meteo')} title="Meteo" />
            </Menu>
            <TouchableOpacity onPress={changeActionApp}>
              <View style={styles.board}>
                <View style={styles.column}>
                  <Text>{selectedAppForAction || 'Selecte une Action'}</Text>
                  {selectedAppForAction == 'WorldTime' && (
                    <>
                      <Menu.Item onPress={() => handleActionSelect('Choisis ton heure')} title="Choisis ton heure" titleStyle={{color: 'black'}} />
                      {selectAction === 'Choisis ton heure' && <WorldTime paramAction={paramAction} setparamAction={setParamAction} />}
                    </>
                  )}
                   {selectedAppForAction == 'Meteo' && (
                    <>
                    <Menu.Item onPress={() => handleActionSelect('Choisis ton temps')} title="Choisis ton temps" titleStyle={{color: 'black'}} />
                    {selectAction === 'Choisis ton temps' && <TimeWeather paramAction={paramAction} setparamAction={setParamAction} />}
                    </>
                  )}
                   {selectedAppForAction == 'Github' && (
                    <>
                    <Menu.Item onPress={() => handleActionSelect('Pull')} title="Pull" titleStyle={{color: 'black'}} />
                    <Menu.Item onPress={() => handleActionSelect('Push')} title="Push" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleActionSelect('Create')} title="Create" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleActionSelect('PullRequestClose')} title="PullRequestClose" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleActionSelect('Nouvelle Branch')} title="Nouvelle Branch" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleActionSelect('Nouveau Collaborator')} title="Nouveau Collaborator" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleActionSelect('Supprime une Branche')} title="Supprime une branche" titleStyle={{color: 'black'}}/>
                    {selectAction === 'Pull' && <PullRequest paramAction={paramAction} setParamAction={setParamAction} />}
                    {selectAction === 'Push' && <PushRequest paramAction={paramAction} setParamAction={setParamAction} />}
                    {selectAction === 'Create' && <CreateRepository setParamAction={setParamAction} />}
                    {selectAction === 'PullRequestClose' && <PullRequestClose paramAction={paramAction} setParamAction={setParamAction} />}
                    {selectAction === 'Nouvelle Branch' && <NewBranch paramAction={paramAction} setParamAction={setParamAction} />}
                    {selectAction === 'Nouveau Collaborator' && <NewCollaborator paramAction={paramAction} setParamAction={setParamAction} />}
                    {selectAction === 'Supprime une Branche' && <DeleteBranch paramAction={paramAction} setParamAction={setParamAction} />}
                    </>
                  )}
                   {selectedAppForAction == 'Spotify' && (
                    <>
                    <Menu.Item onPress={() => handleActionSelect('Nouvelle Track')} title="Nouvelle Track" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleActionSelect('Nouvelle Playlist')} title="Nouvelle Playlist" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleActionSelect('Nouveau Follower')} title="Nouvelle Follower" titleStyle={{color: 'black'}}/>
                    {selectAction === 'Nouvelle Track' && <NewTrack paramAction={paramAction} setParamAction={setParamAction} />}
                    {selectAction === 'Nouvelle Playlist' && <NewPlaylist setParamAction={setParamAction} />}
                    {selectAction === 'Nouveau Follower' && <NewFollower setParamAction={setParamAction} />}
                    </>
                  )}
                   {selectedAppForAction == 'Mail' && (
                    <>
                    </>
                  )}
                   {selectedAppForAction == 'Youtube' && (
                    <>
                      <Menu.Item onPress={() => handleActionSelect('NewVideo')} title="Nouvelle Video" titleStyle={{color: 'black'}}/>
                      <Menu.Item onPress={() => handleActionSelect('NewFollower')} title="Nouveau Follower" titleStyle={{color: 'black'}}/>
                      {selectAction === 'NewVideo' && <NewVideo setParamAction={setParamAction} />}
                      {selectAction === 'NewFollower' && <NewYTFollower setParamAction={setParamAction} />}
                    </>
                  )}
                </View>
                </View>
            </TouchableOpacity>
            <TouchableOpacity onPress={changeReactionApp}>
                <View style={styles.board}>
                <View style={styles.column}>
                  <Text>{selectedAppForReaction || 'Selecte une Reaction'}</Text>
                  {selectedAppForReaction == 'WorldTime' && (
                    <>
                    </>
                  )}
                   {selectedAppForReaction == 'Meteo' && (
                    <>
                    </>
                  )}
                  <View>
                    {selectedAppForReaction === 'Github' && (
                      <>
                      </>
                    )}
                  </View>
                   {selectedAppForReaction == 'Spotify' && (
                    <>
                    <Menu.Item onPress={() => handleReactionSelect('Add song to playlist')} title="Une chanson à ta playlist" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleReactionSelect('FollowArtist')} title="Follow un Artist" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleReactionSelect('FollowePlaylist')} title="Follow une Playlist" titleStyle={{color: 'black'}}/>
                    {selectReaction === 'Add song to playlist' && <AddTracktoPlaylist paramReaction={paramReaction} setParamReaction={setParamReaction} />}
                    {selectReaction === 'FollowArtist' && <FollowArtist paramReaction={paramReaction} setParamReaction={setParamReaction} />}
                    {selectReaction === 'FollowePlaylist' && <FollowPlaylist paramReaction={paramReaction} setParamReaction={setParamReaction} />}
                    </>
                  )}
                   {selectedAppForReaction == 'Mail' && (
                    <>
                    <Menu.Item onPress={() => handleReactionSelect('Remplis ton email')} title="Remplis ton mail" titleStyle={{color: 'black'}}/>
                    {selectReaction === 'Remplis ton email' && <MailWeather paramReaction={paramReaction} setParamReaction={setParamReaction} />}
                    </>
                  )}
                   {selectedAppForReaction == 'Youtube' && (
                    <>
                    <Menu.Item onPress={() => handleReactionSelect('Subscribe')} title="Abonne toi" titleStyle={{color: 'black'}}/>
                    <Menu.Item onPress={() => handleReactionSelect('Add to playlist')} title="Ajoute à la playlist" titleStyle={{color: 'black'}}/>
                    {selectReaction === 'Subscribe' && <Subscribe paramReaction={paramReaction} setParamReaction={setParamReaction} />}
                    {selectReaction === 'Add to playlist' && <AddVideoPlaylist paramReaction={paramReaction} setParamReaction={setParamReaction} />}
                    </>
                  )}
                </View>
              </View>
            </TouchableOpacity>
          </Card.Content>
        </Card>
        <Card style={styles.card}>
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
                <Button onPress={handleSaveClick} buttonColor="grey" textColor='orange'>
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
        </Card>
        <Card style={styles.card}>
          <Card.Content style={styles.cardContent}>
            <View style={styles.editDescriptionContainer}>
              <TextInput
                  style={styles.editDescriptionInput}
                  placeholder="Le nom de ton area"
                  placeholderTextColor="white"
                  value={name}
                  onChangeText={setName}
                />
            </View>
          </Card.Content>
        </Card>
        <Card style={styles.card}>
          <Card.Content style={styles.cardContent}>
            <View style={styles.editDescriptionContainer}>
            <TextInput
              style={styles.editDescriptionInput}
              placeholder={userId === 0 ? 'User ID (Regarde ton profile)' : ''}
              placeholderTextColor="white"
              value={userId === 0 ? '' : userId.toString()}
              onChangeText={(text) => {
                if (text === '') {
                  setUserId(0);
                } else {
                  const parsedUserId = parseInt(text, 10);
                  if (!isNaN(parsedUserId)) {
                    setUserId(parsedUserId);
                  }
                }
              }}
            />
            </View>
          </Card.Content>
        </Card>
        <View>
        <Button style={styles.customButton} onPress={handleCreateClick} textColor='orange'>
          Créer ton Area
        </Button>
        </View>
      </ScrollView>
    </Provider>
  );
}

const styles = StyleSheet.create({
  board: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 20,
    alignItems: 'center',
    marginTop: 20,
    backgroundColor: 'white',
  },
  column: {
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 10,
    paddingHorizontal: 20,
  },
  customButton: {
    backgroundColor: 'grey',
    borderRadius: 5,
    paddingVertical: 12,
    paddingHorizontal: 40,
    marginVertical: 10,
    marginRight: 20,
    alignItems: 'center',
    justifyContent: 'center',
  },
  card : {
    marginBottom: 20,
    width: '95%',
    backgroundColor: 'grey',
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
  cardContent: {
    flexDirection: "column",
    alignItems: "center",
    marginBottom: "5%"
  },
  button: {
    backgroundColor: 'grey',
    padding: 20,
    borderRadius: 5,
    marginBottom: 20,
  },
});

