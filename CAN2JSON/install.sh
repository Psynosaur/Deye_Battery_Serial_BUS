#!/bin/bash

#Require sudo
if [ $EUID != 0 ]; then
    sudo "$0" "$@"
    exit $?
fi

pwd=${pwd}
sed -i "s?placeholder?$PWD?g" can2json.service
sed -i "s?suchuser?$SUDO_USER?g" can2json.service

echo "adding service to /lib/systemd/system/..."
cp can2json.service /etc/systemd/system/
chmod 644 /etc/systemd/system/can2json.service
echo "done"

echo "starting and enabling service..."
systemctl daemon-reload
systemctl enable can2json
systemctl start can2json
echo "done"

echo "Deye BMS Reader installed successfully!"
echo ""
echo "log output can be viewed by running"
echo "sudo journalctl -u can2json.service -f -n"