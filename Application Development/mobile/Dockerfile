FROM node:18

WORKDIR /app

RUN apt-get update -y && apt-get install -y \
  git \
  unzip \
  && apt-get clean

ENV ANDROID_HOME /opt/android
ENV PATH $PATH:$ANDROID_HOME/cmdline-tools/latest/bin
ENV PATH $PATH:$ANDROID_HOME/platform-tools

RUN mkdir -p $ANDROID_HOME && cd $ANDROID_HOME && \
  curl -o cmdline-tools.zip https://dl.google.com/android/repository/commandlinetools-linux-7583922_latest.zip && \
  unzip cmdline-tools.zip && \
  rm cmdline-tools.zip

RUN mkdir -p $ANDROID_HOME/licenses
RUN echo -e "\n8933bad161af4178b1185d1a37fbf41ea5269c55" > $ANDROID_HOME/licenses/android-sdk-license
RUN echo -e "\n84831b9409646a918e30573bab4c9c91346d8abd" > $ANDROID_HOME/licenses/android-sdk-preview-license
RUN echo -e "\nd975f751698a77b662f1254ddbeed3901e976f5a3" > $ANDROID_HOME/licenses/intel-android-extra-license

RUN apt-get install -y gradle

WORKDIR /app

COPY package*.json ./

RUN npm install -g react-native-cli

RUN npm install

COPY . .

RUN react-native bundle --platform android --dev false --entry-file /app/tabs/index.tsx --bundle-output android/app/src/main/assets/index.android.bundle --assets-dest android/app/src/main/res --verbose

RUN cd android && ./gradlew assembleRelease

RUN mkdir -p /apk
RUN cp ./android/app/build/outputs/apk/release/app-release.apk /apk/app-release.apk

RUN rm -rf $ANDROID_HOME
RUN rm -rf /var/lib/apt/lists/*


CMD [ "echo", "APK build completed. You can find the APK in /apk directory." ]

