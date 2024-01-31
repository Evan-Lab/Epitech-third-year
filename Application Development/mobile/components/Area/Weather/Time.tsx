import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramAction: { hour?: number; minute?: number; city?: string };
    setparamAction: (paramAction: { hour?: number; minute?: number; city?: string }) => void;
}

export default function TimeWeather({ paramAction, setparamAction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Enter la ville"
                value={paramAction.city ?? ""}
                onChangeText={(text) => setparamAction({ ...paramAction, city: text })}
                style={styles.button}
            />
            <TextInput
                placeholder="Entre l'heure"
                value={paramAction.hour?.toString() ?? ""}
                onChangeText={(text) => {
                    const parsedHour = parseInt(text, 10) || 0;
                    setparamAction({ ...paramAction, hour: parsedHour });
                }}
                style={styles.button}
            />
            <TextInput
                placeholder="Enter la minute"
                value={paramAction.minute?.toString() ?? ""}
                onChangeText={(text) => {
                    const parsedMinute = parseInt(text, 10) || 0;
                    setparamAction({ ...paramAction, minute: parsedMinute });
                }}
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
