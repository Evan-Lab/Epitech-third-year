import {
    ApiEmailConfirmation,
    ApiInfo,
    ApiLogin,
    ApiRegister,
    TokenLogin,
    ApiWeather,
    ApiPullRepository,
    ApiPushRepository,
    ApiCreateRepository,
    ApiPlaylistSpotify,
    ApiFollowerSpotify,
    ApiTrackSpotify,
    ApiFacebook,
    ApiGithub,
    ApiGoogle,
    ApiGoogleToken,
    ApiCreate,
    ApiAdmin,
    ApiDeleteAdmin,
    ApiChangePassword,
    ApiArea,
} from './api';
import axios, { AxiosError } from "axios"
import { ExpoRoot } from 'expo-router';
import { Alert } from 'react-native';

export type ApiRequest<T> =
    | {
        data: T
        error: null
    }
    | {
        error: string
    }

const url = process.env.EXPO_PUBLIC_API_URL
const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMiIsIm5iZiI6MTY5OTE1OTY3NiwiZXhwIjoxNjk5MjQ2MDc2LCJpYXQiOjE2OTkxNTk2NzZ9.KJ5-G-f7qfDOqWfd1V-I-wgqSlWY3dCdaHL7ZD7FOsI"

const request = {
    connection: {
        login: async (data: ApiLogin): Promise<ApiRequest<TokenLogin>> => {
            try {
                const headers = {
                    'Content-type': 'application/json',
                    'Accept': 'application/json',
                }
                const response = await axios.post(`${url}/auth/login?email=${data.email}&password=${data.password}`);
                return {
                    data: response.data,
                    error: null,
                }
            } catch (error) {
                return {
                    error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce ton email et ton mot de passe'
                }
            }
        }
    },
    register: async (data: ApiRegister): Promise<ApiRequest<ApiRegister>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.post(`${url}/auth/register`, {
                email: data.email,
                password: data.password,
                firstname: data.firstname,
                lastname: data.lastname,
            }, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            return {
                error: 'Quelque chose ne va pas. Vérifie que tu as bien rempli tous les champs et que ton mot de passe',
            }
        }
    },
    emailConfirmation: async (set: boolean, email: string): Promise<ApiRequest<ApiEmailConfirmation>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.get(`${url}/auth/confirmation?set=${set}&email=${email}`, {
                params: {
                    set,
                    email: email
                },
                headers,
            });
            if (response.status === 200) {
                Alert.alert('Email Confirmation', 'You have successfully confirmed your email.');
                return {
                    data: response.data,
                    error: null,
                }
            }
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie tes mails',
            }
        } catch (error) {
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie tes mails',
            }
        }
    },

    infoProfile: async (token: string): Promise<ApiRequest<ApiInfo>> => {
        try {
            const headers = {
                Authorization: `Bearer ${token}`,
            }
            const response = await axios.get(`${url}/user/me`, {
                headers,
            });
            console.log(response.data)
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            return {
                error: 'Quelque chose ne va pas. Reviens sur la page et vérifie que tu es encore connecté',
            }
        }
    },
    weatherCity: async (data: ApiWeather): Promise<ApiRequest<ApiWeather>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.post(`${url}/area/tester`, {
                email: data.email,
                city: data.city,
                hour: data.hour,
                minute: data.minute,
            }, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    pullRepository: async (data: ApiPullRepository): Promise<ApiRequest<ApiPullRepository>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.post(`${url}/services/pullRepository`, {
                repository: data.repository,
            }, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    pushRepository: async (data: ApiPushRepository): Promise<ApiRequest<ApiPushRepository>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.post(`${url}/services/pushRepository`, {
                repository: data.repository,
            }, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    createRepository: async (data: ApiCreateRepository): Promise<ApiRequest<ApiCreateRepository>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.post(`${url}/services/createRepository`, {
                //repository: data.repository,
            }, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    playlistSpotify: async (data: ApiPlaylistSpotify): Promise<ApiRequest<ApiPlaylistSpotify>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.post(`${url}/services/playlistSpotify`, {
                // email: data.email,
            }, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    followerSpotify: async (data: ApiFollowerSpotify): Promise<ApiRequest<ApiFollowerSpotify>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.post(`${url}/services/followerSpotify`, {
                //email: data.email,
            }, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    trackSpotify: async (data: ApiTrackSpotify): Promise<ApiRequest<ApiTrackSpotify>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            const response = await axios.post(`${url}/services/trackSpotify`, {
                playlistId: data.playlistId,
            }, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    googleLogin: async (data: ApiGoogleToken): Promise<ApiRequest<ApiGoogleToken>> => {
        console.log('goole login', data)
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            console.log(data.pageName)
            const response = await axios.post(`${url}/google/token`, {
                pageName: data.pageName,
                code: data.code,
            }, {
                headers,
            });
            console.log(response.data)
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    googleCode: async (data: ApiGoogle): Promise<ApiRequest<ApiGoogle>> => {
        console.log('page google ', data)
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            console.log(data.pageName)
            const response = await axios.post(`${url}/google/link`, {
                pageName: data.pageName,
            }, {
                headers,
            });
            console.log(response.data)
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log((error as AxiosError).request)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    githubLogin: async (data: ApiGithub): Promise<ApiGithub | null> => {
        try {
          const headers = {
            'Content-type': 'application/json',
            'Accept': 'application/json',
          };
          console.log('GitHub Login Data:', data); // Log the data object
          const response = await axios.post(`${url}/auth/github`, { code: data.code }, { headers });
          console.log('GitHub Login Response:', response.data); // Log the response data
          if (response.status === 200) {
            const responseData = response.data as ApiGithub;
            return responseData;
          } else {
            console.error('Invalid response status:', response.status);
            return null;
          }
        } catch (error) {
          console.error('Error during GitHub login:', error);
          throw new Error('Something went wrong. Please retry your request!');
        }
      },
    facebookLogin: async (data: ApiFacebook): Promise<ApiRequest<ApiFacebook>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            console.log(data.code)
            const response = await axios.post(`${url}/auth/facebook`, {
                code: data.code,
            }, {
                headers,
            });
            console.log(response.data)
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ce que tu écris',
            }
        }
    },
    createArea : async (data: ApiCreate): Promise<ApiRequest<ApiCreate>> => {
        console.log("data is ", data)
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
                Authorization: `Bearer ${token}`,
            }
            const response = await axios.post(`${url}/area/create`, {
                name: data.name,
                userId: data.userId,
                actionId: data.actionId,
                reactionId: data.reactionId,
                paramAction: data.paramAction,
                paramReaction: data.paramReaction,
            }, {
                headers,
            });
            console.log(response.data)
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Une erreur est survenue. Vérifie que tu as bien rempli tous les champs',
            }
        }
    },
    giveAdmin : async (data: ApiAdmin): Promise<ApiRequest<ApiAdmin>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            console.log(data)
            const response = await axios.post(`${url}/user/set-admin`, {
                idUser: data.idUser,
            }, {
                headers,
            });
            console.log(response.data)
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Vérifie que tu as les droits pour faire ça',
            }
        }
    },
    deleteAdmin : async (data: ApiDeleteAdmin): Promise<ApiRequest<ApiDeleteAdmin>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
            }
            console.log(data)
            const response = await axios.post(`${url}/user/remove-admin`, {
                idUser: data.idUser,
            }, {
                headers,
            });
            console.log(response.data)
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Vérifie que tu as les droits pour faire ça',
            }
        }
    },
    changePassword : async (data: ApiChangePassword): Promise<ApiRequest<ApiChangePassword>> => {
        try {
            const headers = {
                'Content-type': 'application/json',
                'Accept': 'application/json',
                Authorization: `Bearer ${token}`,
            }
            const response = await axios.post(`${url}/user/resetPwd?Id=${data.Id}&Password=${data.Password}`, null, {
                headers,
            });
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            return {
                error: 'Quelque chose ne va pas. Vérifie ton mot de passe et ta confirmation',
            }
        }
    },
    areaList : async (): Promise<ApiRequest<ApiArea[]>> => {
        try {
            const headers = {
                Authorization: `Bearer ${token}`,
            }
            const response = await axios.get(`${url}/area/listArea`, {
                headers,
            });
            console.log(response.data)
            return {
                data: response.data,
                error: null,
            }
        } catch (error) {
            console.log(error)
            return {
                error: 'Quelque chose ne va pas. Recommence ta requête et vérifie ta création de ActionReaction',
            }
        }
    },

}


export default request;