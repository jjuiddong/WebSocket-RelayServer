//------------------------------------------------------------------------
// Name:    relay_ProtocolHandler.h
// Author:  ProtocolGenerator (by jjuiddong)
// Date:    
//------------------------------------------------------------------------
#pragma once

#include "relay_ProtocolData.h"

namespace relay {

using namespace network2;
using namespace marshalling;
static const int c2s_Dispatcher_ID = 100;

// Protocol Dispatcher
class c2s_Dispatcher: public network2::cProtocolDispatcher
{
public:
	c2s_Dispatcher();
protected:
	virtual bool Dispatch(cPacket &packet, const ProtocolHandlers &handlers) override;
};
static c2s_Dispatcher g_relay_c2s_Dispatcher;


// ProtocolHandler
class c2s_ProtocolHandler : virtual public network2::iProtocolHandler
{
	friend class c2s_Dispatcher;
	virtual bool Relay(relay::Relay_Packet &packet) { return true; }
};


}
