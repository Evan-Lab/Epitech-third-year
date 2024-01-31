import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramAction: { url?: string };
    setParamAction: (param: { url: string }) => void;
}

export default function NewTrack({ paramAction, setParamAction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Enter ta Spotify track URL"
                value={paramAction.url ?? ""}
                onChangeText={(s) => setParamAction({ url: s })}
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