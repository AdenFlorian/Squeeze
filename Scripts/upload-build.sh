#!/bin/bash

zipPath="../Builds/$version/$version.zip"
uploadTarget="bundt@squeeze.AdenFlorian.com:~/squeeze/update_packages/."

echo "Which version do you want to upload: "

read version

if [ -f "$zipPath" ];
then
	echo "Uploading $zipPath to $uploadTarget"
	scp $zipPath $uploadTarget
	echo "Upload complete!"
else
	echo "File $zipPath does not exist" >&2
fi

read -n 1 -s -p "Press any key to continue"
