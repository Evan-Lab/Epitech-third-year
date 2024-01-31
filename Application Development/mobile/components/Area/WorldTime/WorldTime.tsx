import React from 'react';
import { View, TextInput, StyleSheet } from 'react-native';

interface Props {
    paramAction: { day?: number, month?:number, minuteParam?: number, hourParam?:number, year?:number };
    setparamAction: (paramAction: { day?: number, month?:number, minuteParam?: number, hourParam?:number, year?:number }) => void;
}

export default function WorldTime({ paramAction, setparamAction }: Props) {
    return (
        <View>
            <TextInput
                placeholder="Entre le jour"
                value={paramAction.day?.toString() ?? ""}
                onChangeText={(text) => {
                    const parsedDay = parseInt(text, 10) || 0;
                    setparamAction({ ...paramAction, day: parsedDay });
                }}
                style={styles.button}
            />
            <TextInput
                placeholder="Entre le mois"
                value={paramAction.month?.toString() ?? ""}
                onChangeText={(text) => {
                    const parsedMonth = parseInt(text, 10) || 0;
                    setparamAction({ ...paramAction, month: parsedMonth });
                }}
                style={styles.button}
            />
            <TextInput
                placeholder="Entre l'année"
                value={paramAction.year?.toString() ?? ""}
                onChangeText={(text) => {
                    const parsedYear = parseInt(text, 10) || 0;
                    setparamAction({ ...paramAction, year: parsedYear });
                }}
                style={styles.button}
            />
            <TextInput
                placeholder="Entre l'heure"
                value={paramAction.hourParam?.toString() ?? ""}
                onChangeText={(text) => {
                    const parsedHour = parseInt(text, 10) || 0;
                    setparamAction({ ...paramAction, hourParam: parsedHour });
                }}
                style={styles.button}
            />
            <TextInput
                placeholder="Entre la minute"
                value={paramAction.minuteParam?.toString() ?? ""}
                onChangeText={(text) => {
                    const parsedHour = parseInt(text, 10) || 0;
                    setparamAction({ ...paramAction, minuteParam: parsedHour });
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
