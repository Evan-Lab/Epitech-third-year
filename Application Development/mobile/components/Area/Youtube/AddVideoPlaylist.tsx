import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramReaction: { urlVideo?: string; urlPlaylist?: string };
    setParamReaction: (param: { urlVideo?: string; urlPlaylist?: string }) => void;
}

export default function AddVideoPlaylist({ paramReaction, setParamReaction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Entre ton url video"
                value={paramReaction.urlVideo ?? ""}
                onChangeText={(s) => setParamReaction({ ...paramReaction, urlVideo: s })}
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
