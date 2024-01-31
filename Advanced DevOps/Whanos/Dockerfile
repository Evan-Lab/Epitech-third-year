FROM jenkins/jenkins:lts-jdk11
COPY --from=docker /usr/local/bin/docker /usr/local/bin/docker

USER root

ARG ADMIN_PASSWORD

ENV JENKINS_ADMIN_PASSWORD $ADMIN_PASSWORD

ENV JAVA_OPTS -Djenkins.install.runSetupWizard=false

COPY jenkins/plugins.txt /usr/share/jenkins/ref/plugins.txt
COPY jenkins/job_dsl.groovy /var/jenkins_home/job_dsl.groovy

COPY jenkins  /jenkins
COPY images /images

RUN jenkins-plugin-cli -f /usr/share/jenkins/ref/plugins.txt

ENV CASC_JENKINS_CONFIG /jenkins/config.yaml
