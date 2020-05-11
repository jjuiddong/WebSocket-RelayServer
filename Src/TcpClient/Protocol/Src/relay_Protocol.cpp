#include "stdafx.h"
#include "relay_Protocol.h"
using namespace relay;

//------------------------------------------------------------------------
// Protocol: Relay
//------------------------------------------------------------------------
void relay::c2s_Protocol::Relay(netid targetId, const string &str)
{
	cPacket packet(m_node->GetPacketHeader());
	packet.SetProtocolId( GetId() );
	packet.SetPacketId( 1334027723 );
	packet << str;
	AddDelimeter(packet);
	packet.EndPack();
	GetNode()->Send(targetId, packet);
}



