# tutor-site

To copy a file from the server to your computer, this [instruction](https://stackoverflow.com/questions/30553428/copying-files-from-server-to-local-computer-using-ssh) will help you to

An example of how you can take data from the server:

scp root@:HOST_DEPLOY_IP/tutorproPl/HOST_ADMIN_USER/proj/TutorPro/appsettings.Production.json path/to/copy

To copy data from the docker to the server and back, this [instruction](https://zomro.com/ua/blog/faq/284-kak-skopirovat-dannye-s-hosta-v-docker-i-iz-docker-na-host) will tell you how to do it. It's important to note that this must be done on the server