import QtQuick 2.3
import QtQuick.Window 2.2

Window {
    visible: true
    color: red

    MouseArea {
        anchors.fill: parent
        onClicked: {
            Qt.quit();
        }
    }

    Text {
        text: qsTr("Hello World,中国")
        anchors.centerIn: parent
    }
}

