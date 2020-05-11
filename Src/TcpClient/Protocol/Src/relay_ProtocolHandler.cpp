#include "stdafx.h"
#include "relay_ProtocolHandler.h"

using namespace relay;


relay::c2s_Dispatcher::c2s_Dispatcher()
	: cProtocolDispatcher(relay::c2s_Dispatcher_ID)
{
	cProtocolDispatcher::GetDispatcherMap()->insert({c2s_Dispatcher_ID, this });
}

//------------------------------------------------------------------------
// 패킷의 프로토콜에 따라 해당하는 핸들러를 호출한다.
//------------------------------------------------------------------------
bool relay::c2s_Dispatcher::Dispatch(cPacket &packet, const ProtocolHandlers &handlers)
{
	const int protocolId = packet.GetProtocolId();
	const int packetId = packet.GetPacketId();
	switch (packetId)
	{
	case 1334027723:
		{
			ProtocolHandlers prtHandler;
			if (!HandlerMatching<c2s_ProtocolHandler>(handlers, prtHandler))
				return false;

			SetCurrentDispatchPacket( &packet );

			Relay_Packet data;
			data.pdispatcher = this;
			data.senderId = packet.GetSenderId();
			packet >> data.str;
			SEND_HANDLER(c2s_ProtocolHandler, prtHandler, Relay(data));
		}
		break;

	default:
		assert(0);
		break;
	}
	return true;
}



