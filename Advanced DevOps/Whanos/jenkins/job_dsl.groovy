folder("Whanos base images") {
    description("Folder to stock whanos base images")
}

folder("Projects") {
    description("Folder to stock projects")
}

languages = ["c", "java", "javascript", "python", "befunge"]

for (lang in languages) {
    freeStyleJob("Whanos base images/whanos-${lang}")
    {
        steps {
            shell("docker build -t whanos-${lang} - < /images/${lang}/Dockerfile.base")
        }

        triggers {
            upstream("Build all base images")
        }
    }
}

freeStyleJob("Whanos base images/Build all base images") { }

def jobCreatedByLinkProject = '''
    freeStyleJob(\"Projects/$JOB_NAME\")
    {
        wrappers {
            preBuildCleanup()
        }
        scm {
            github(GIT_REPOSITORY_URL.trim())
        }

        triggers {
            cron("* * * * *")
        }

        steps {
            shell(\"\"\"
            #!/bin/bash
            /jenkins/deploy.sh Projects/$JOB_NAME
            \"\"\")
        }
    }
'''

freeStyleJob("link-project")
{
    parameters {
        stringParam('GIT_REPOSITORY_URL', null, "Git URL of the repository to link")
        stringParam("JOB_NAME", null, "Display name for the job")
    }

    steps {
        dsl {
            text(jobCreatedByLinkProject)
        }
    }
}