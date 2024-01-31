import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramAction: { name?: string };
    setParamAction: (param: { name: string }) => void;
}

export default function NewBranch({ paramAction, setParamAction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Entre ton nom de branche"
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
        justifyContent: 'center',
    },
    buttonText: {
        fontSize: 20,
        color: '#fff',
    },
});