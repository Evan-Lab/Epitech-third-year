import React, { useState, useEffect } from 'react'
import { View, StyleSheet, Text } from 'react-native';

import LogoPeople from './HomeLogo/LogoPeople';
import LogoPeopleTwo from './HomeLogo/LogoPeopleTwo';
import LogoPeopleThree from './HomeLogo/LogoPeopleThree';
import LogoPeopleFour from './HomeLogo/LogoPeopleFour';
import LogoPeopleFive from './HomeLogo/LogoPeopleFive';
import Logo from './HomeLogo/logo';
import LogoHome from './HomeLogo/LogoHome';

const backgroundStyle = {
  backgroundColor: '#4d4d4c',
  flex: 1,
};

const GetUser = async () => {
    const authToken = 'gbow2QTCOc-rPjcWkNnsKw_a3vuw1DdE';
    const accessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6NzQsImVtYWlsIjoib2xpdmVyLmxld2lzQG1hc3VyYW8uanAiLCJuYW1lIjoiT2xpdmVyIiwic3VybmFtZSI6Ikxld2lzIiwiZXhwIjoxNjk1OTc4ODcyfQ.064Dz25zxIo5sdZ2px0tTT4iEq0Nhbg9oGi5bt_NnoQ';
  
    try {
      const response = await fetch('https://masurao.fr/api/employees/me', {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          'X-Group-Authorization': authToken,
          Authorization: 'Bearer ' + accessToken,
        },
      });
  
      if (response.ok) {
        const json = await response.json();
        return json;
      } else {
        throw new Error('Réponse non OK');
      }
    } catch (error) {
      console.error('Erreur lors de la récupération des données de l\'utilisateur :', error);
      throw error;
    }
};

const User = () => {
    const [userData, setUserData] = useState(null);

    useEffect(() => {
        GetUser()
        .then(data => {
            setUserData(data);
        })
        .catch(error => {
            console.error('Erreur lors de la récupération des données de l\'utilisateur :', error);
        });
    }, []);

    return (
        <View style={backgroundStyle}>
            <Logo />
            <LogoPeople
                width={100}
                height={30}
                left={50}
                top={100}
            />
            <LogoPeopleTwo
                width={100}
                height={30}
                left={30}
                top={300}
            />
            <LogoPeopleThree
                width={100}
                height={30}
                left={260}
                top={300}
            />
            <LogoPeopleFour
                width={100}
                height={30}
                left={30}
                top={500}
            />
            <LogoPeopleFive
                width={100}
                height={30}
                left={260}
                top={500}
            />

            {userData ? (
            <>
                <Text style={{ fontSize: 23, height: 30, top: -110, left: 170, fontWeight: 'bold' }}>{userData.surname}</Text>
                <Text style={{ fontSize: 23, height: 30, top: -110, left: 170, fontWeight: 'bold' }}>{userData.name}</Text>
                <Text style={{ fontSize: 23, height: 30, top: -110, left: 170, fontWeight: 'bold' }}>{userData.birth_date}</Text>
                <Text style={{ fontSize: 23, height: 30, top: -110, left: 170, fontWeight: 'bold' }}>{userData.work}</Text>
                <Text style={{ fontSize: 23, height: 30, top: -110, left: 150, fontWeight: 'bold' }}>{userData.email}</Text>
            </>
            ) : (
              <Text>Chargement en cours...</Text>
            )}

            <Text style={{ fontSize: 30, height: 50, top: -70, left: 125, textDecorationLine: 'underline' }}>Subordonnés</Text>

            <Text style={{ fontSize: 23, height: 30, top: 70, left: 50, fontWeight: 'bold' }}>Nom</Text>
            <Text style={{ fontSize: 23, height: 30, top: 70, left: 40, fontWeight: 'bold' }}>Prénom</Text>
            <Text style={{ fontSize: 23, height: 30, top: 10, left: 280, fontWeight: 'bold' }}>Nom</Text>
            <Text style={{ fontSize: 23, height: 30, top: 10, left: 270, fontWeight: 'bold' }}>Prénom</Text>

            <Text style={{ fontSize: 23, height: 30, top: 140, left: 50, fontWeight: 'bold' }}>Nom</Text>
            <Text style={{ fontSize: 23, height: 30, top: 140, left: 40, fontWeight: 'bold' }}>Prénom</Text>
            <Text style={{ fontSize: 23, height: 30, top: 80, left: 280, fontWeight: 'bold' }}>Nom</Text>
            <Text style={{ fontSize: 23, height: 30, top: 80, left: 270, fontWeight: 'bold' }}>Prénom</Text>
        </View>
    );
}

export default User;