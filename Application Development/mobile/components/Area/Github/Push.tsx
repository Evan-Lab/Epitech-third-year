import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramAction: { name?: string };
    setParamAction: (param: { name: string }) => void;
}

export default function PushRequest({ paramAction, setParamAction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Entre ton nom de repo"
                value={paramAction && paramAction.name ? paramAction.name : ""}
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