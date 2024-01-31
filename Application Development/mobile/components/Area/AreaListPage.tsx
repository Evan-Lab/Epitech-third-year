import * as React from 'react';
import { View, Text, FlatList, StyleSheet } from 'react-native';
import { Card } from 'react-native-paper';
import request from '../../constants/request';
import CreateArea from './CreateArea';
import { useEffect, useState } from 'react';
import { ScrollView } from 'react-native-gesture-handler';

type Area = {
    name: string;
    dateCreation: string;
    isActive: number;
};

export default function AreaListPage() {
    const [area, setArea] = useState<Area[]>([]);
    const [error, setError] = useState<string | null>(null);

    const fetchAreas = async () => {
      try {
        const response = await request.areaList();
        if (response.error === null) {
          setError(null);
          if (response.data) {
            setArea(response.data);
          }
        }
      } catch (error) {
        console.error('AreaList Error:', error);
      }
    };

    useEffect(() => {
        fetchAreas();
      }, []);

    return (
        <ScrollView>
        <View>
            <Text>List of Areas:</Text>
            {area?.map((item, num) => (
                <Card style={styles.card} key={`${item.name}-${num}`}>
                    <Card.Content>
                        <Text>Name: {item.name}</Text>
                        <Text>Creation Date: {item.dateCreation.slice(0, 10)}</Text>
                        <Text>Active: {item.isActive ? 'Yes' : 'No'}</Text>
                    </Card.Content>
                </Card>
            ))}
        </View>
        </ScrollView>
    );
}

const styles = StyleSheet.create({
    card : {
        margin: 10,
        marginBottom: 10,
    }
});
