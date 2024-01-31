import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramReaction: { url?: string };
    setParamReaction: (param: { url: string }) => void;
}

export default function FollowPlaylist({ paramReaction, setParamReaction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Entre ta playlist url"
                value={paramReaction.url ?? ""}
                onChangeText={(s) => setParamReaction({ url: s })}
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
