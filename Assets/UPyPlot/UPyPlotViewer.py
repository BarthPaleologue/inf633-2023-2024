#! /usr/bin/python

__author__ = "Grant Olsen"
__copyright__ = "Copyright 2017, Grant Olsen"

__license__ = "GPL"
__version__ = "1.0.0"
__maintainer__ = "Grant Olsen"
__email__ = "jython.scripts@gmail.com"
__status__ = "Beta"

#--------------------------------#
# Plot viewer for visualizing Unity script variables in realtime
#--------------------------------#

import numpy as np
import matplotlib.pyplot as plt
import matplotlib.animation as animation

fig = plt.figure("UPyPlot Window")
fig.set_facecolor((0.63, 0.63, 0.63))

ax1 = fig.add_subplot(1,1,1)
ax1.set_facecolor((0.87, 0.87, 0.87))

def animate(i):
    pollData = open("plotting_cache/plot.txt","r").read() ### for mac
    # pollData = open("plotting_cache\plot.txt","r").read() ### for windows
    try: #try to unpack the data in the file, if for any reason it fails then just drop everything and return to start a new cycle.
        dataArray = pollData.split('\n')[2:]
        dataHeader = pollData.split('\n')[1].split(',')
        dataMeta = pollData.split('\n')[0].split(',')
    except:
        return
    
    print(dataMeta[0] + " : " + dataMeta[1] + " : " + dataMeta[2])

    try: #try to cast the strings to numeric representaton, if there was a value error then just drop everything and return to start a new cycle.
        currentSample = int(dataMeta[0])
        gameTime = float(dataMeta[1])
    except ValueError:
        return

    nElements = int(dataMeta[0]) #dataHeader.__len__()
    yElements = [np.array([]) for x in range(nElements)]

    for i, eachLine in enumerate(dataArray):
        if len(eachLine) > 1:
            #print(eachLine)
            for n, val in enumerate(eachLine.split(',')):
                yElements[n] = np.append(yElements[n], val)
            #print(yElements)

    xar = np.linspace(gameTime - (i * 0.1), gameTime, i)
    if xar.shape[0] == currentSample:
        ax1.clear()
        for n, yax in enumerate(yElements):
            if (n % 2 == 1):
                continue
            try: # try to display this axis, if the data is corrupt then just skip it this frame.
                ax1.plot(xar, yax, label=dataHeader[n % 2])
            except:
                pass
        #ax1.legend(loc='upper left', fontsize=7)

        ax1.relim()
        # update ax.viewLim using the new dataLim
        ax1.autoscale_view()

ani = animation.FuncAnimation(fig, animate, interval=100)
plt.show()