echo "Creating folder wow434"
mkdir wow434
cd wow434

echo "Downloading files..."
HOST_URL="http://blizzard.vo.llnwd.net:80/o16/content/repair/wow/"
curl ${HOST_URL}E/1/E1FC69A72E4E23A96DBD535B372974A8 > "BackgroundDownloader.exe"
curl ${HOST_URL}2/4/24433A51A32335A39D2AF8CB55C467D3 > "Battle.net.dll"
curl ${HOST_URL}8/2/82EF43D5F8D1B1C87C3505ECD241FFF6 > "Blizzard Updater.exe"
curl ${HOST_URL}4/0/4003E34416EBD25E4C115D49DC15E1A7 > "dbghelp.dll"
curl ${HOST_URL}5/7/57E72CAE12091DAFA29A8E4DB8B4F1D1 > "divxdecoder.dll"
curl ${HOST_URL}D/3/D34B3DA03C59F38A510EAA8CCC151EC7 > "Microsoft.VC80.CRT.manifest"
curl ${HOST_URL}1/1/1169436EE42F860C7DB37A4692B38F0E > "msvcr80.dll"
curl ${HOST_URL}D/E/DE5A2E274F2D3F2B89A2E6EC9CD8FD2A > "Wow.exe"
curl ${HOST_URL}7/8/78766BBBFC6F9E5DA5D930CB11F0A1E1 > "WowError.exe"
curl ${HOST_URL}E/1/E198F00FE056B24ED58B36E1C6A048F4 > "Repair.exe"
wget http://eu.media.battle.net.edgesuite.net/downloads/wow-installers/live/WoWLive-64-Win-15595.zip
unzip WoWLive-64-Win-15595.zip
rm WoWLive-64-Win-15595.zip

echo "Done!"

