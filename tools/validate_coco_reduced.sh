#!/bin/bash
# VALIDATE OVER REDUCED SDS DATASET (CROSS)

CROSS=size_5
LIBPATH=lib/yolov5-master
DATASET=seadronesee_cross/$CROSS
NAME=coco_pretrained_sds_${CROSS}
MODELPATH=$LIBPATH/yolov5s.pt

python $LIBPATH/val.py \
    --data datasets/$DATASET/data_coco.yaml \
    --name $NAME \
    --weights $MODELPATH \
    --save-txt \
    --save-json \
    --save-conf
