#!/bin/bash

#Require sudo
if [ $EUID != 0 ]; then
    sudo "$0" "$@"
    exit $?
fi

echo "removing service..."
systemctl stop can2json
systemctl disable can2json
echo "done"

echo "removing service from /etc/systemd/system/..."
rm /etc/systemd/system/can2json.service
echo "done"

echo "reloading services"
systemctl daemon-reload
echo "done"

echo "Deye BMS Reader uninstalled successfully!"
echo "Huzzah"