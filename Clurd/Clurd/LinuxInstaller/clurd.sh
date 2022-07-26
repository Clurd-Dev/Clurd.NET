#! /bin/bash

### BEGIN INIT INFO
# Provides:          Clurd
# Required-Start:    $local_fs $network
# Required-Stop:     $local_fs
# Default-Start:     2 3 4 5
# Default-Stop:      0 1 6
# Short-Description: Clurd server
# Description:       Run Clurd server
### END INIT INFO

# Carry out specific functions when asked to by the system
case "$1" in
  start)
    echo "Starting Clurd server"
    sudo -u USER bash -c 'cd PATHTOCLURD & nohup ./Clurd --urls="http://0.0.0.0:5000" &> log.out &'
    ;;
  stop)
    echo "Stopping Clurd server"
    sudo -u USER bash -c 'fuser -k 5000/tcp'
    sleep 2
    ;;
  *)
    echo "Usage: /etc/init.d/clurd {start|stop}"
    exit 1
    ;;
esac

exit 0