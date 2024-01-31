import React from "react";
import { StyleSheet, TouchableOpacity, Pressable } from "react-native";
import Animated,{ FadeInDown, FadeOutDown} from 'react-native-reanimated'
import { Image } from "expo-image";

import { BACKGROUND_COLOR_WIDGET, SIZE_MEDIUM, SHADOW_COLOR } from '../../Variable';

const ImageNews = require('../../assets/nouvelles.png')
const ImageCroix = require('../../assets/croix-petit.png')

export default function WidgetNews(props) {

    return (
        <Animated.View
            entering={FadeInDown}
            exiting={FadeOutDown}
            style={styles.container}
        >
            <Pressable onPress={() => props.changeModalNews(true)}>
            <TouchableOpacity style={styles.button} onPress={() => props.updState()}>
              <Image
                  style={styles.imageCroix}
                  source={ImageCroix}
                  contentFit="cover"
                  transition={1000}
              />
            </TouchableOpacity>
            <Image
                    style={styles.imageNews}
                    source={ImageNews}
                    contentFit="cover"
                    transition={1000}
            />
            </Pressable>
        </Animated.View>
    );
}

const styles = StyleSheet.create({
    container: {
        width: 170,
        height: 158,
        borderRadius: 30,
        backgroundColor: BACKGROUND_COLOR_WIDGET,
        shadowColor: SHADOW_COLOR,
        shadowOffset: {width: -2, height: 4},
        shadowOpacity: 0.2,
        shadowRadius: 3,
    },
    imageNews: {
        width: 80,
        height: 80,
        marginLeft: 45,
    },
    imageCroix: {
        width: 30,
        height: 30,
    },
    button: {
        color: 'grey',
        fontSize: SIZE_MEDIUM,
        width: 50,
        height: 50,
        border: 'none',
        borderRadius: 50,
        backgroundColor: 'transparent',
        alignItems: 'center',
        justifyContent: 'center',
        marginRight: 30,
    },
});
