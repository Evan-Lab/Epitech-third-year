#!/bin/bash

FILES=("Makefile", "app/pom.xml", "package.json", "requirements.txt", "app/main.bf")

LANGUAGE=null

if [[ -f Makefile ]];then
    LANGUAGE=c
    echo "$LANGUAGE";
fi

if [[ -f app/pom.xml ]];then
    LANGUAGE=java
    echo "$LANGUAGE";
fi

if [[ -f package.json ]];then
    LANGUAGE=javascript
    echo "$LANGUAGE";
fi

if [[ -f requirements.txt ]];then
    LANGUAGE=python
    echo "$LANGUAGE";
fi

if [[ -f app/main.bf ]]; then
    LANGUAGE=befunge
    echo "$LANGUAGE";
fi

if [[ LANGUAGE == null ]]; then
    echo "No compatible language found"
    exit 1
fi

if [[ -f Dockerfile ]]; then
    docker build . -t whanos-$1
else
    docker build . -f /images/${LANGUAGE}/Dockerfile.standalone -t whanos-$1
fi