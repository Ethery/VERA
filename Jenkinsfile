pipeline {
	agent {
		node {
			label 'Unity'
		}
	}
	parameters {
		choice(name: 'BUILD_TARGET', choices: ['StandaloneWindows64', 'StandaloneOSX', 'StandaloneWindows', 'iOS', 'Android', 'WebGL', 'WSAPlayer', 'StandaloneLinux64', 'PS4', 'XboxOne', 'tvOS', 'Switch', 'Stadia', 'LinuxHeadlessSimulation', 'PS5', 'VisionOS'])
		text(name:"UNITY_VERSION", defaultValue:"2022.3.28f1")
	}
	stages { 
		stage('Initialize Unity Project') {
			steps {
				buildName "${BUILD_NUMBER}(${GIT_COMMIT})"
				writeFile(file: "Unity/Assets/StreamingAssets/version.txt", text: "${BUILD_NUMBER}.${GIT_COMMIT}")
				writeFile(file: "Unity/Assets/StreamingAssets/ActiveEnvironment.txt", text: "Main")
				sh '/opt/Unity/Editor/${UNITY_VERSION}/Editor/Unity -quit -accept-apiupdate -nographics -batchmode -logFile - -projectPath "${WORKSPACE}/Unity"'
			}
		}
		stage('Build Unity') {
			
			steps {
				sh '/opt/Unity/Editor/${UNITY_VERSION}/Editor/Unity -quit -accept-apiupdate -nographics -batchmode -logFile - -buildTarget ${BUILD_TARGET} -executeMethod GameBuilder.Build ${WORKSPACE_TMP} -projectPath "${WORKSPACE}/Unity"'
			}
		}
		stage('Create archive') {
			steps {
				sh '''cd ${WORKSPACE_TMP}
				zip -r ${WORKSPACE}/${BUILD_TAG}_Game.zip Game'''
			}
		}
	}
    post {
        always {
            archiveArtifacts artifacts: 'Game.zip', fingerprint: true, onlyIfSuccessful: true
        }
    }
}