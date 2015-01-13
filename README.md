# PeerCom

- Multipeer Connectivitiy Sample


# Multipeer Connectivity Framework Reference

From [iOS Developer Library](https://developer.apple.com/library/prerelease/ios/documentation/MultipeerConnectivity/Reference/MultipeerConnectivityFramework/index.html)

    IMPORTANT

        This is a preliminary document for an API or technology in development. 
        Apple is supplying this information to help you plan for the adoption of the technologies 
        and programming interfaces described herein for use on Apple-branded products. 

        This information is subject to change, and software implemented according 
        to this document should be tested with final operating system software 
        and final documentation. 

        Newer versions of this document may be provided with future betas of the API or technology.


## Architecture

When working with the Multipeer Connectivity framework, 
your app must interact with several types of objects, 
as described below.

- Session objects (MCSession) 
  provide support for communication between connected peer devices. 
  If your app creates a session, 
  it can invite other peers to join it. 

  Otherwise, your app can join a session when invited by another peer.

- Advertiser objects (MCNearbyServiceAdvertiser) 
  tell nearby peers that your app is willing to join sessions of a specified type.

- Advertiser assistant objects (MCAdvertiserAssistant) 
  provide the same functionality as advertiser objects, 
  but also provide a standard user interface 
  that allows the user to accept invitations. 
  
  If you wish to provide your own user interface, 
  or if you wish to exercise additional programmatic control 
  over which invitations are displayed, 
  use an advertiser object directly.
  
- Browser objects (MCNearbyServiceBrowser) 
  let your app search programmatically for nearby devices 
  with apps that support sessions of a particular type.
  
- Browser view controller objects (MCBrowserViewController) 
  provide a standard user interface 
  that allows the user to choose nearby peers to add to a session.
  
- Peer IDs (MCPeerID) uniquely identify an app 
  running on a device to nearby peers.

Session objects maintain a set of peer ID objects 
that represent the peers connected to the session. 

Advertiser objects also use a single local peer object 
to provide information that identifies the device 
and its user to other nearby devices.

## Using the Framework

This framework is used in two phases: 
the **discovery phase**, and the **session phase**.

### Deicovery Phase ###

In the discovery phase, 
your app uses a browser object 
(described in MCNearbyServiceBrowser Class Reference) 
to browse for nearby peers, 
optionally using the provided view controller (described in MCBrowserViewController Class Reference) to display a user interface.

The app also uses an advertiser object 
(described in MCNearbyServiceAdvertiser Class Reference) 
or an advertiser assistant object 
(described in MCAdvertiserAssistant Class Reference) to tell nearby peers that it is available so that apps on other nearby devices can invite it to a session.

During the discovery phase, 
your app has limited communication with and knowledge of other peers; 
it has access to the discoveryInfo data that other nearby clients provide, 
and any context data that other peers provide when inviting it to join a session.


#### Invitation ####

After the user chooses which peers to add to a session, the app invites those peers to join the session. 

Apps running on the nearby devices can choose whether to accept or reject the invitation, 
and can ask their users for permission.

### Session Phase ###

If the peer accepts the invitation, the browser establishes a connection with the advertiser and the session phase begins. In this phase, your app can perform direct communication to one or more peers within the session. The framework notifies your app through delegate callbacks when peers join the session and when they leave the session.


## Classes


### MCAdvertiserAssistant

The MCAdvertiserAssistant is a convenience class that handles advertising, presents incoming invitations to the user and handles users’ responses.

### MCNearbyServiceAdvertiser

The MCNearbyServiceAdvertiser class publishes an advertisement for a specific service that your app provides through the Multipeer Connectivity framework and notifies its delegate about invitations from nearby peers.

### MCNearbyServiceBrowser

Searches (by service type) for services offered by nearby devices using infrastructure Wi-Fi, peer-to-peer Wi-Fi, and Bluetooth, and provides the ability to easily invite those devices to a Multipeer Connectivity session (MCSession).

### MCPeerID

The MCPeerID class represents a peer in a multipeer session.

### MCSession

An MCSession object enables and manages communication among all peers in a Multipeer Connectivity session.

### MCBrowserViewController

The MCBrowserViewController class presents nearby devices to the user and enables the user to invite nearby devices to a session.

## Protocol

### MCAdvertiserAssistantDelegate

The MCAdvertiserAssistantDelegate protocol describes the methods that the delegate object for an MCAdvertiserAssistant instance can implement to handle advertising-related events.

### MCBrowserViewControllerDelegate

The MCBrowserViewControllerDelegate protocol defines the methods that your delegate object can implement to handle events related to the MCBrowserViewController class.

### MCNearbyServiceAdvertiserDelegate

The MCNearbyServiceAdvertiserDelegate protocol describes the methods that the delegate object for an MCNearbyServiceAdvertiser instance can implement for handling events from the MCNearbyServiceAdvertiser class.

### MCNearbyServiceBrowserDelegate

The MCNearbyServiceBrowserDelegate protocol defines methods that a MCNearbyServiceBrowser object’s delegate can implement to handle browser-related events.

### MCSessionDelegate

The MCSessionDelegate protocol defines methods that a delegate of the MCSession class can implement to handle session-related events.