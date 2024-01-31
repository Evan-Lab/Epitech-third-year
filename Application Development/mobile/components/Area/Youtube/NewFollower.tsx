import React from 'react';
import { View, Button, StyleSheet, TextInput } from 'react-native';

interface Props {
    setParamAction: (paramAction: { hour?: number; minute?: number; url?: string; city?: string; name?: string }) => void;
}

export default function NewYTFollower({ setParamAction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Entre rien"
                onChangeText={() => setParamAction({})}
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