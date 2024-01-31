import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramAction: { name?: string };
    setParamAction: (param: { name: string }) => void;
}

export default function DeleteBranch({ paramAction, setParamAction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Ta branche à supprimer"
                value={paramAction.name ?? ""}
                onChangeText={(s) => setParamAction({ name: s })}
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