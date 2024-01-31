import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramReaction: { urlTrack?: string; urlPlaylist?: string };
    setParamReaction: (param: { urlTrack?: string; urlPlaylist?: string }) => void;
}

export default function AddTracktoPlaylist({ paramReaction, setParamReaction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Entre ton url track"
                value={paramReaction.urlTrack ?? ""}
                onChangeText={(s) => setParamReaction({ ...paramReaction, urlTrack: s })}
                style={styles.button}
            />
            <TextInput
                placeholder="Entre ton url playlist"
                value={paramReaction.urlPlaylist ?? ""}
                onChangeText={(s) => setParamReaction({ ...paramReaction, urlPlaylist: s })}
                style={styles.button}
            />
        </View>
    );
}

const styles = StyleSheet.create({
    button: {
        backgroundColor: 'grey',
        padding: 20,
        borderRadius: 5,
        marginBottom: 20,
    },
    buttonText: {
        fontSize: 20,
        color: '#fff',
    },
});