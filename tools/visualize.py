import os
import matplotlib.pyplot as plt
import matplotlib.patches as patches
import pandas as pd

DIR = os.path.join("/Users","jorge", "prueba-crest", "dataset")

SUPERSIZE = 4 # TODO from config?

# read annotations
labels = pd.read_csv(os.path.join(DIR, "debug.txt"), header=None)

for _, row in labels.iterrows():
    im_path = os.path.join(DIR, "screenshot{}.png".format(int(row[0])))
    im = plt.imread(im_path)
    im_h, im_w, _ = im.shape

    # display im in subplot figure
    fig, ax = plt.subplots()
    ax.imshow(im)

    # flip vertical axis
    xmax, ymax, xmin, ymin = row[1:5] * SUPERSIZE
    ymax = im_h - ymax 
    ymin = im_h - ymin # true ymax

    # add bbox
    w = abs(xmax - xmin) 
    h = abs(ymax - ymin)

    padding = 0
    originX = min(xmin, xmax) - padding # min(row[1], row[3]) - padding 
    originY = min(ymin, ymax) - padding # min(row[2], row[4]) - padding

    rect = patches.Rectangle((originX, originY), w + 2 * padding, h + 2 * padding, linewidth=1, edgecolor='r', facecolor='none')
    ax.add_patch(rect)

    w = 1
    h = 1

    rect = patches.Rectangle((xmax, ymax), w + 2 * padding, h + 2 * padding, linewidth=1, edgecolor='r', facecolor='none', color='yellow')
    ax.add_patch(rect)

    rect = patches.Rectangle((xmin, ymin), w + 2 * padding, h + 2 * padding, linewidth=1, edgecolor='r', facecolor='none')
    ax.add_patch(rect)

    # hold plot
    plt.show()
