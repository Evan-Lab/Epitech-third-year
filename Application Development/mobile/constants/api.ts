export type ApiLogin = {
    email: string;
    password: string;
};

export type ApiRegister = {
    email: string;
    password: string;
    firstname: string;
    lastname: string;
};

export type ApiEmailConfirmation = {
    response: string;
};

export type ApiInfo = {
    firstname: string;
    lastname: string;
    email: string;
    dateCreation: string;
    id: string;
};

export type TokenLogin = {
    token: string;
};

export type ApiWeather = {
    email: string;
    city: string;
    hour: number;
    minute: number;
};

export type ApiPullRepository = {
    repository: string;
};

export type ApiPushRepository = {
    repository: string;
};

export type ApiCreateRepository = {
    repository: string;
};

export type ApiPlaylistSpotify = {

};

export type ApiFollowerSpotify = {

};

export type ApiTrackSpotify = {
    playlistId: string;
};

export type ApiGoogle = {
    pageName: string;
};

export type ApiGoogleToken = {
    code: string | null;
    pageName: string;
};

export type ApiFacebook = {
    code: string;
};

export type ApiGithub = {
    code: string;
};

export type ApiCreate = {
    name: string;
    userId: number;
    actionId: number;
    reactionId: number;
    paramAction: string;
    paramReaction: string;
};

export type ApiAdmin = {
    idUser: number;
};

export type ApiDeleteAdmin = {
    idUser: number;
};

export type ApiChangePassword = {
    Id: string;
    Password: string;
};

export type ApiArea = {
    name: string;
    dateCreation: string;
    isActive: number;
};